using ImportData;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void T3_SupAgreementsImport()
        {
            var xlsxPath = TestSettings.SupagreementsPathXlsx;
            var action = ImportData.Constants.Actions.ImportSupAgreements;
            var sheetName = ImportData.Constants.SheetNames.SupAgreements;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedSupAgreement in items)
            {
                var error = EqualsSupAgreement(expectedSupAgreement);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsSupAgreement(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var regNumber = parameters[shift + 0].Trim();
            var regDate = Common.ParseDate(parameters[shift + 1].Trim());
            var regNumberLeadingDocument = parameters[shift + 2].Trim();
            var regDateLeadingDocument = Common.ParseDate(parameters[shift + 3].Trim());
            var actualSupAgreement = BusinessLogic.GetEntityWithFilter<ISupAgreements>(d => d.RegistrationNumber == regNumber &&
                                                                                            d.RegistrationDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") ==
                                                                                            regDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'"),
                                                                                            exceptionList, TestSettings.Logger, true);
            var leadingDocument = BusinessLogic.GetEntityWithFilter<IContracts>(d => d.RegistrationNumber == regNumberLeadingDocument && 
                                                                                     d.RegistrationDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'") == 
                                                                                     regDateLeadingDocument.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'"), 
                                                                                     exceptionList, TestSettings.Logger, true);


            if (actualSupAgreement == null)
                return $"Не найдено дополнительное соглашение";

            if (leadingDocument == null)
                return $"Не найден ведущий документ";

            var errorList = new List<string>
            {
                Common.CheckParam(actualSupAgreement.RegistrationNumber, parameters[shift + 0].Trim(), "RegistrationNumber"),
                Common.CheckParam(actualSupAgreement.RegistrationDate, Common.ParseDate(parameters[shift + 1].Trim()), "RegistrationDate"),
                Common.CheckParam(leadingDocument.RegistrationNumber, parameters[shift + 2].Trim(), "LeadingDocumentRegNumber"),
                Common.CheckParam(leadingDocument.RegistrationDate, Common.ParseDate(parameters[shift + 3].Trim()), "LeadingDocumentRegNumberRegDate"),
                Common.CheckParam(actualSupAgreement.Counterparty == null ? string.Empty : actualSupAgreement.Counterparty.Name, parameters[shift + 4].Trim(), "Counterparty"),
                Common.CheckParam(actualSupAgreement.DocumentKind == null ? string.Empty : actualSupAgreement.DocumentKind.Name , parameters[shift + 5].Trim(), "DocumentKind"),
                Common.CheckParam(actualSupAgreement.Subject, parameters[shift + 6].Trim(), "Subject"),
                Common.CheckParam(actualSupAgreement.BusinessUnit == null ? string.Empty : actualSupAgreement.BusinessUnit.Name, parameters[shift + 7].Trim(), "BusinessUnit"),
                Common.CheckParam(actualSupAgreement.Department == null ? string.Empty : actualSupAgreement.Department.Name, parameters[shift + 8].Trim(), "Department"),
                Common.CheckParam(actualSupAgreement.LastVersion().Body.Value.SequenceEqual(File.ReadAllBytes(parameters[shift + 9].Trim())), "LastVersion"),
                Common.CheckParam(actualSupAgreement.ValidFrom, Common.ParseDate(parameters[shift + 10].Trim()), "ValidFrom"),
                Common.CheckParam(actualSupAgreement.ValidTill, Common.ParseDate(parameters[shift + 11].Trim()), "ValidTill"),
                Common.CheckParam(actualSupAgreement.TotalAmount, parameters[shift + 12].Trim(), "TotalAmount"),
                Common.CheckParam(actualSupAgreement.Currency == null ? string.Empty : actualSupAgreement.Currency.Name, parameters[shift + 13].Trim(), "Currency"),
                Common.CheckParam(actualSupAgreement.LifeCycleState, BusinessLogic.GetPropertyLifeCycleState(parameters[shift + 14].Trim()), "LifeCycleState"),
                Common.CheckParam(actualSupAgreement.ResponsibleEmployee ==  null ? string.Empty : actualSupAgreement.ResponsibleEmployee.Name , parameters[shift + 15].Trim(), "ResponsibleEmployee"),
                Common.CheckParam(actualSupAgreement.OurSignatory == null ? string.Empty : actualSupAgreement.OurSignatory.Name, parameters[shift + 16].Trim(), "OurSignatory"),
                Common.CheckParam(actualSupAgreement.Note, parameters[shift + 17].Trim(), "Note"),
                Common.CheckParam(actualSupAgreement.DocumentRegister?.Id, parameters[shift + 18].Trim(), "DocumentRegister"),
                Common.CheckParam(actualSupAgreement.RegistrationState, BusinessLogic.GetRegistrationsState(parameters[shift + 19].Trim()), "RegistrationState")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности:");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}