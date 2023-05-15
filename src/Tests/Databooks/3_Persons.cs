using ImportData;

namespace Tests.Databooks
{
    public partial class Tests
    {
        [Fact]
        public void T3_PersonImport()
        {
            var xlsxPath = TestSettings.PersonsPathXlsx;
            var action = ImportData.Constants.Actions.ImportPersons;
            var sheetName = ImportData.Constants.SheetNames.Persons;
            var logger = TestSettings.Logger;

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedPerson in Common.XlsxParse(xlsxPath, sheetName, logger))
            {
                var error = EqualsPerson(expectedPerson);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsPerson(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = string.Join(' ', parameters[shift + 0].Trim(), parameters[shift + 1].Trim(), parameters[shift + 2].Trim()).Trim();
            var actualPerson = BusinessLogic.GetEntityWithFilter<IPersons>(x => x.Name == name, exceptionList, TestSettings.Logger, true);

            if (actualPerson == null)
                return $"Не найдена персона {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualPerson.LastName, parameters[shift + 0].Trim(), "LastName"),
                Common.CheckParam(actualPerson.FirstName, parameters[shift + 1].Trim(), "FirstName"),
                Common.CheckParam(actualPerson.MiddleName, parameters[shift + 2].Trim(), "MiddleName"),
                // Пол определяется прикладной логикой.
                //Common.CheckParam(actualPerson.Sex, BusinessLogic.GetPropertySex((parameters[shift + 3].Trim()), "Sex"),
                Common.CheckParam(actualPerson.DateOfBirth == null ? string.Empty : actualPerson.DateOfBirth.Value.ToString("dd.MM.yyyy"), parameters[shift + 4].Trim(), "DateOfBirth"),
                Common.CheckParam(actualPerson.TIN, parameters[shift + 5].Trim(), "TIN"),
                Common.CheckParam(actualPerson.INILA, parameters[shift + 6].Trim(), "INILA"),
                Common.CheckParam(actualPerson.City == null ? string.Empty : actualPerson.City.Name, parameters[shift + 7].Trim(), "City"),
                Common.CheckParam(actualPerson.Region == null ? string.Empty : actualPerson.Region.Name, parameters[shift + 8].Trim(), "Region"),
                Common.CheckParam(actualPerson.LegalAddress, parameters[shift + 9].Trim(), "LegalAddress"),
                Common.CheckParam(actualPerson.PostalAddress, parameters[shift + 10].Trim(), "PostalAddress"),
                Common.CheckParam(actualPerson.Phones, parameters[shift + 11].Trim(), "Phones"),
                Common.CheckParam(actualPerson.Email, parameters[shift + 12].Trim(), "Email"),
                Common.CheckParam(actualPerson.Homepage, parameters[shift + 13].Trim(), "Homepage"),
                Common.CheckParam(actualPerson.Bank == null ? string.Empty : actualPerson.Bank.Name, parameters[shift + 14].Trim(), "Bank"),
                Common.CheckParam(actualPerson.Account, parameters[shift + 15].Trim(), "Account"),
                Common.CheckParam(actualPerson.Note, parameters[shift + 16].Trim(), "Note")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}