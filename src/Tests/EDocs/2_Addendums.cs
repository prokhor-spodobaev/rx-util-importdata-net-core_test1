using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData;
using Xunit.Extensions.Ordering;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact, Order(20)]
        public void T2_AddendumsImport()
        {
            Assert.Fail("Приложения не поддерживаются");

            var xlsxPath = TestSettings.AddendumsPathXlsx;
            var action = ImportData.Constants.Actions.ImportAddendums;
            var sheetName = ImportData.Constants.SheetNames.Addendums;

            var items = Common.XlsxParse(xlsxPath, sheetName);

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedAddendum in items)
            {
                var error = EqualsAddendums(expectedAddendum);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsAddendums(List<string> parameters, int shift = 0)
        {
            var actualAddendum = Common.GetOfficialDocument<IAddendums>(parameters[shift + 0], parameters[shift + 1]);
            var leadingDocument = Common.GetOfficialDocument<IAddendums>(parameters[shift + 2], parameters[shift + 3]);

            if (actualAddendum == null)
                return $"Не найдено дополнительное соглашение";

            var errorList = new List<string>
            {
                Common.CheckParam(actualAddendum.RegistrationNumber, parameters[shift + 0], "RegistrationNumber"),
                Common.CheckParam(actualAddendum.RegistrationDate, parameters[shift + 1], "RegistrationDate"),
                Common.CheckParam(leadingDocument.RegistrationNumber, parameters[shift + 2], "LeadingDocumentRegNumber"),
                Common.CheckParam(leadingDocument.RegistrationDate, parameters[shift + 3], "LeadingDocumentRegNumberRegDate"),
                //Common.CheckParam(actualAddendum.Counterparty, parameters[shift + 4], "Counterparty"),
                Common.CheckParam(actualAddendum.DocumentKind, parameters[shift + 5], "DocumentKind"),
                Common.CheckParam(actualAddendum.Subject, parameters[shift + 6], "Subject"),
                Common.CheckParam(actualAddendum.BusinessUnit, parameters[shift + 7], "BusinessUnit"),
                Common.CheckParam(actualAddendum.Department, parameters[shift + 8], "Department"),
                Common.CheckParam(actualAddendum.LastVersion(), parameters[shift + 9], "LastVersion"),
                Common.CheckParam(actualAddendum.LifeCycleState, BusinessLogic.GetPropertyLifeCycleState(parameters[shift + 10]), "LifeCycleState"),
                //Common.CheckParam(actualAddendum.ResponsibleEmployee, parameters[shift + 11], "ResponsibleEmployee"),
                Common.CheckParam(actualAddendum.OurSignatory, parameters[shift + 12], "OurSignatory"),
                Common.CheckParam(actualAddendum.Note, parameters[shift + 13], "Note"),
                Common.CheckParam(actualAddendum.DocumentRegister?.Id, parameters[shift + 14], "DocumentRegister"),
                Common.CheckParam(actualAddendum.RegistrationState, BusinessLogic.GetRegistrationsState(parameters[shift + 15]), "RegistrationState")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности:");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}
