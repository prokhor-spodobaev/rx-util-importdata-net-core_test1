using ImportData;

namespace Tests.Databooks
{
    public partial class Tests
    {
        [Fact]
        public void T1_CompanyImport()
        {
            var xlsxPath = TestSettings.CompanyPathXlsx;
            var action = ImportData.Constants.Actions.ImportCompany;
            var sheetNameEmployees = ImportData.Constants.SheetNames.Employees;
            var sheetNameBusinessUnits = ImportData.Constants.SheetNames.BusinessUnits;
            var sheetNameDepartments = ImportData.Constants.SheetNames.Departments;
            var logger = TestSettings.Logger;

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            //Проверка работников.
            foreach (var expectedEmployee in Common.XlsxParse(xlsxPath, sheetNameEmployees, logger))
            {
                var error = EqualsEmployee(expectedEmployee);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }

            //Проверка наших организаций.
            foreach (var expectedBusinessUnit in Common.XlsxParse(xlsxPath, sheetNameBusinessUnits, logger))
            {
                var error = EqualsBusinessUnit(expectedBusinessUnit);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }

            //Проверка подразделений.
            foreach (var expectedDepartment in Common.XlsxParse(xlsxPath, sheetNameDepartments, logger))
            {
                var error = EqualsDepartaments(expectedDepartment);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }

            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsEmployee(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = string.Join(' ', parameters[shift + 2].Trim(), parameters[shift + 3].Trim(), parameters[shift + 4].Trim()).Trim();
            var actualEmployee = BusinessLogic.GetEntityWithFilter<IEmployees>(x => x.Name == name, exceptionList, TestSettings.Logger, true);
            var actualPerson = BusinessLogic.GetEntityWithFilter<IPersons>(x => x.Name == name, exceptionList, TestSettings.Logger, true);
            if (actualEmployee == null)
                return $"Не найден сотрудник {name}";
            if (actualPerson == null)
                return $"Не найдена персона {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualEmployee.Department == null ? string.Empty : actualEmployee.Department.Name, parameters[shift + 0].Trim(), "Department"),
                Common.CheckParam(actualEmployee.JobTitle == null ? string.Empty: actualEmployee.JobTitle.Name, parameters[shift + 1].Trim(), "JobTitle"),
                Common.CheckParam(actualPerson.LastName, parameters[shift + 2].Trim(), "LastName"),
                Common.CheckParam(actualPerson.FirstName, parameters[shift + 3].Trim(), "FirstName"),
                Common.CheckParam(actualPerson.MiddleName, parameters[shift + 4].Trim(), "MiddleName"),
                Common.CheckParam(actualPerson.Sex, BusinessLogic.GetPropertySex(parameters[shift + 5].Trim()), "Sex"),
                Common.CheckParam(actualPerson.DateOfBirth == null ? string.Empty : actualPerson.DateOfBirth.Value.ToString("dd.MM.yyyy"), parameters[shift + 6].Trim(), "DateOfBirth"),
                Common.CheckParam(actualPerson.TIN, parameters[shift + 7].Trim(), "TIN"),
                Common.CheckParam(actualPerson.INILA, parameters[shift + 8].Trim(), "INILA"),
                Common.CheckParam(actualPerson.City == null ? string.Empty : actualPerson.City.Name, parameters[shift + 9].Trim(), "City"),
                Common.CheckParam(actualPerson.Region == null ? string.Empty : actualPerson.Region.Name, parameters[shift + 10].Trim(), "Region"),
                Common.CheckParam(actualPerson.LegalAddress, parameters[shift + 11].Trim(), "LegalAddress"),
                Common.CheckParam(actualPerson.PostalAddress, parameters[shift + 12].Trim(), "PostalAddress"),
                Common.CheckParam(actualEmployee.Phone, parameters[shift + 13].Trim(), "Phone"),
                Common.CheckParam(actualEmployee.Email, parameters[shift + 14].Trim(), "Email"),
                Common.CheckParam(actualPerson.Homepage, parameters[shift + 15].Trim(), "Homepage"),
                Common.CheckParam(actualPerson.Bank == null ? string.Empty : actualPerson.Bank.Name, parameters[shift + 16].Trim(), "Bank"),
                Common.CheckParam(actualPerson.Account, parameters[shift + 17].Trim(), "Account"),
                Common.CheckParam(actualEmployee.Note, parameters[shift + 18].Trim(), "Note"),
                
                Common.CheckParam(actualEmployee.NeedNotifyExpiredAssignments.ToString(), (false).ToString(), "NeedNotifyExpiredAssignments"),
                Common.CheckParam(actualEmployee.NeedNotifyNewAssignments.ToString(), (!string.IsNullOrWhiteSpace(actualEmployee.Email)).ToString(), "NeedNotifyNewAssignments"),
                Common.CheckParam(actualEmployee.NeedNotifyAssignmentsSummary.ToString(), (!string.IsNullOrWhiteSpace(actualEmployee.Email)).ToString(), "NeedNotifyAssignmentsSummary")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }

        public static string EqualsBusinessUnit(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = parameters[shift + 0].Trim();
            var actualBusinessUnit = BusinessLogic.GetEntityWithFilter<IBusinessUnits>(x => x.Name == name, exceptionList, TestSettings.Logger, true);

            if (actualBusinessUnit == null)
                return $"Не найдена наша организация {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualBusinessUnit.Name, parameters[shift + 0].Trim(), "Name"),
                Common.CheckParam(actualBusinessUnit.LegalName, parameters[shift + 1].Trim(), "LegalName"),
                Common.CheckParam(actualBusinessUnit.HeadCompany == null ? string.Empty : actualBusinessUnit.HeadCompany.Name, parameters[shift + 2].Trim(), "HeadCompany"),
                Common.CheckParam(actualBusinessUnit.CEO == null ? string.Empty : actualBusinessUnit.CEO.Name, parameters[shift + 3].Trim(), "CEO"),
                Common.CheckParam(actualBusinessUnit.Nonresident.ToString(), (parameters[shift + 4].Trim().ToLower() == "да").ToString(), "Nonresident"),
                Common.CheckParam(actualBusinessUnit.TIN, parameters[shift + 5].Trim(), "TIN"),
                Common.CheckParam(actualBusinessUnit.TRRC, parameters[shift + 6].Trim(), "TRRC"),
                Common.CheckParam(actualBusinessUnit.PSRN, parameters[shift + 7].Trim(), "PSRN"),
                Common.CheckParam(actualBusinessUnit.NCEO, parameters[shift + 8].Trim(), "NCEO"),
                Common.CheckParam(actualBusinessUnit.NCEA, parameters[shift + 9].Trim(), "NCEA"),
                Common.CheckParam(actualBusinessUnit.City == null ? string.Empty : actualBusinessUnit.City.Name, parameters[shift + 10].Trim(), "City"),
                Common.CheckParam(actualBusinessUnit.Region == null ? string.Empty : actualBusinessUnit.Region.Name, parameters[shift + 11].Trim(), "Region"),
                Common.CheckParam(actualBusinessUnit.LegalAddress, parameters[shift + 12].Trim(), "LegalAddress"),
                Common.CheckParam(actualBusinessUnit.PostalAddress, parameters[shift + 13].Trim(), "PostalAddress"),
                Common.CheckParam(actualBusinessUnit.Phones, parameters[shift + 14].Trim(), "Phones"),
                Common.CheckParam(actualBusinessUnit.Email, parameters[shift + 15].Trim(), "Email"),
                Common.CheckParam(actualBusinessUnit.Homepage, parameters[shift + 16].Trim(), "Homepage"),
                Common.CheckParam(actualBusinessUnit.Note, parameters[shift + 17].Trim(), "Note"),
                Common.CheckParam(actualBusinessUnit.Account, parameters[shift + 18].Trim(), "Account"),
                Common.CheckParam(actualBusinessUnit.Bank == null ? string.Empty : actualBusinessUnit.Bank.Name, parameters[shift + 19].Trim(), "Bank")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }

        public static string EqualsDepartaments(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = parameters[shift + 0].Trim();
            var actualDepartment = BusinessLogic.GetEntityWithFilter<IDepartments>(x => x.Name == name, exceptionList, TestSettings.Logger, true);

            if (actualDepartment == null)
                return $"Не найдено подразделение {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualDepartment.Name, parameters[shift + 0].Trim(), "Name"),
                Common.CheckParam(actualDepartment.ShortName, parameters[shift + 1].Trim(), "ShortName"),
                Common.CheckParam(actualDepartment.Code, parameters[shift + 2].Trim(), "Code"),
                Common.CheckParam(actualDepartment.BusinessUnit == null ? string.Empty : actualDepartment.BusinessUnit.Name, parameters[shift + 3].Trim(), "BusinessUnit"),
                Common.CheckParam(actualDepartment.HeadOffice == null ? string.Empty : actualDepartment.HeadOffice.Name, parameters[shift + 4].Trim(), "HeadOffice"),
                Common.CheckParam(actualDepartment.Manager == null ? string.Empty : actualDepartment.Manager.Name , parameters[shift + 5].Trim(), "Manager"),
                Common.CheckParam(actualDepartment.Phone, parameters[shift + 6].Trim(), "Phone"),
                Common.CheckParam(actualDepartment.Note, parameters[shift + 7].Trim(), "Note")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}