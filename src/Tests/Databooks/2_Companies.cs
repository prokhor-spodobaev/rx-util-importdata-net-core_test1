using ConfigSettings.Patch;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using ImportData;

namespace Tests.Databooks
{
    public partial class Tests
    {
        [Fact]
        public void T2_CompaniesImport()
        {
            var xlsxPath = TestSettings.CompaniesPathXlsx;
            var action = ImportData.Constants.Actions.ImportCompanies;
            var sheetName = ImportData.Constants.SheetNames.Companies;
            var logger = TestSettings.Logger;

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedCompany in Common.XlsxParse(xlsxPath, sheetName, logger))
            {
                var error = EqualsCompany(expectedCompany);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsCompany(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = parameters[shift + 0].Trim();
            var actualCompany = BusinessLogic.GetEntityWithFilter<ICompanies>(x => x.Name == name, exceptionList, TestSettings.Logger, true);

            if (actualCompany == null)
                return $"Не найдена компания {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualCompany.Name, parameters[shift + 0].Trim(), "Name"),
                Common.CheckParam(actualCompany.LegalName, parameters[shift + 1].Trim(), "LegalName"),
                Common.CheckParam(actualCompany.HeadCompany == null ? string.Empty : actualCompany.HeadCompany.Name, parameters[shift + 2].Trim(), "HeadCompany"),
                Common.CheckParam(actualCompany.Nonresident.ToString(), (parameters[shift + 3].Trim().ToLower() == "да").ToString(), "Nonresident"),
                Common.CheckParam(actualCompany.TIN, parameters[shift + 4].Trim(), "TIN"),
                Common.CheckParam(actualCompany.PSRN, parameters[shift + 5].Trim(), "PSRN"),
                Common.CheckParam(actualCompany.NCEO, parameters[shift + 6].Trim(), "NCEO"),
                Common.CheckParam(actualCompany.NCEA, parameters[shift + 7].Trim(), "NCEA"),
                Common.CheckParam(actualCompany.TRRC, parameters[shift + 8].Trim(), "TRRC"),
                Common.CheckParam(actualCompany.City == null ? string.Empty : actualCompany.City.Name, parameters[shift + 9].Trim(), "City"),
                //Регион подставляется прикладной по городу, не проверяем.
                //Common.CheckParam(actualCompany.Region == null ? string.Empty : actualCompany.Region.Name, parameters[shift + 10].Trim(), "Region"),
                Common.CheckParam(actualCompany.LegalAddress, parameters[shift + 11].Trim(), "LegalAddress"),
                Common.CheckParam(actualCompany.PostalAddress, parameters[shift + 12].Trim(), "PostalAddress"),
                Common.CheckParam(actualCompany.Phones, parameters[shift + 13].Trim(), "Phones"),
                Common.CheckParam(actualCompany.Email, parameters[shift + 14].Trim(), "Email"),
                Common.CheckParam(actualCompany.Homepage, parameters[shift + 15].Trim(), "Homepage"),
                Common.CheckParam(actualCompany.Note, parameters[shift + 16].Trim(), "Note"),
                Common.CheckParam(actualCompany.Account, parameters[shift + 17].Trim(), "Account"),
                Common.CheckParam(actualCompany.Bank == null ? string.Empty : actualCompany.Bank.Name, parameters[shift + 18].Trim(), "Bank"),
                Common.CheckParam(actualCompany.Responsible == null ? string.Empty : actualCompany.Responsible.Name, parameters[shift + 19].Trim(), "Responsible")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}
