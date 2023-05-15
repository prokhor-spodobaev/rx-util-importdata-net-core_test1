using System.Globalization;
using System.Security.Cryptography;
using DocumentFormat.OpenXml.VariantTypes;
using ImportData;
using ImportData.Entities.Databooks;
using ImportData.IntegrationServicesClient.Models;
using System.Linq;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void T1_ContractsImport()
        {
            var xlsxPath = TestSettings.ContractsPathXlsx;
            var action = ImportData.Constants.Actions.ImportContracts;
            var sheetName = ImportData.Constants.SheetNames.Contracts;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

            // Создание категорий договора из файла.
            foreach (var item in items)
            {
                CreateDocumentGroup(item);
            }

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedContract in items)
            {
                var error = EqualsContract(expectedContract);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsContract(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var regNumber = parameters[shift + 0].Trim();

            DateTimeOffset regDate = DateTimeOffset.MinValue;

            var regDateBeginningOfDay = Common.ParseDate(parameters[shift + 1].Trim());
            var counterpartyName = parameters[shift + 2].Trim();
            var documentRegisterId = -1;
            int.TryParse(parameters[shift + 17].Trim(), out documentRegisterId);


            var actualContract = BusinessLogic.GetEntityWithFilter<IContracts>(x => x.RegistrationNumber != null &&
                                                                                    x.RegistrationNumber == regNumber &&
                                                                                    x.RegistrationDate == regDateBeginningOfDay &&
                                                                                    x.Counterparty.Name == counterpartyName &&
                                                                                    x.DocumentRegister.Id == documentRegisterId, exceptionList, TestSettings.Logger, true);
        
            if (actualContract == null)
                return $"Не найден договор";

            var errorList = new List<string>
            {
                Common.CheckParam(actualContract.RegistrationNumber, parameters[shift + 0].Trim(), "RegistrationNumber"),
                Common.CheckParam(actualContract.RegistrationDate, Common.ParseDate(parameters[shift + 1].Trim()), "RegistrationDate"),
                Common.CheckParam(actualContract.Counterparty == null ? string.Empty : actualContract.Counterparty.Name, parameters[shift + 2].Trim(), "Counterparty"),
                Common.CheckParam(actualContract.DocumentKind == null ? string.Empty : actualContract.DocumentKind.Name, parameters[shift + 3].Trim(), "DocumentKind"),
                Common.CheckParam(actualContract.DocumentGroup == null ? string.Empty : actualContract.DocumentGroup.Name, parameters[shift + 4].Trim(), "DocumentGroup"),
                Common.CheckParam(actualContract.Subject, parameters[shift + 5].Trim(), "Subject"),
                Common.CheckParam(actualContract.BusinessUnit == null ? string.Empty : actualContract.BusinessUnit.Name, parameters[shift + 6].Trim(), "BusinessUnit"),
                Common.CheckParam(actualContract.Department == null ? string.Empty : actualContract.Department.Name, parameters[shift + 7].Trim(), "Department"),
                Common.CheckParam(actualContract.LastVersion().Body.Value.SequenceEqual(File.ReadAllBytes(parameters[shift + 8].Trim())), "LastVersion"),
                Common.CheckParam(actualContract.ValidFrom, Common.ParseDate(parameters[shift + 9].Trim()), "ValidFrom"),
                Common.CheckParam(actualContract.ValidTill, Common.ParseDate(parameters[shift + 10].Trim()), "ValidTill"),
                Common.CheckParam(actualContract.TotalAmount, parameters[shift + 11].Trim(), "TotalAmount"),
                Common.CheckParam(actualContract.Currency == null ? string.Empty : actualContract.Currency.Name, parameters[shift + 12].Trim(), "Currency"),
                Common.CheckParam(actualContract.LifeCycleState, BusinessLogic.GetPropertyLifeCycleState(parameters[shift + 13].Trim()), "LifeCycleState"),
                Common.CheckParam(actualContract.ResponsibleEmployee == null ? string.Empty : actualContract.ResponsibleEmployee.Name, parameters[shift + 14].Trim(), "ResponsibleEmployee"),
                Common.CheckParam(actualContract.OurSignatory == null ? string.Empty : actualContract.OurSignatory.Name, parameters[shift + 15].Trim(), "OurSignatory"),
                Common.CheckParam(actualContract.Note, parameters[shift + 16].Trim(), "Note"),
                Common.CheckParam(actualContract.DocumentRegister?.Id, parameters[shift + 17].Trim(), "DocumentRegister"),
                Common.CheckParam(actualContract.RegistrationState, BusinessLogic.GetRegistrationsState(parameters[shift + 18].Trim()), "RegistrationState")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности:");

            return string.Join(Environment.NewLine, errorList);
        }

        public static IContractCategories CreateDocumentGroup(List<string> parameters, int shift = 0)
        {
            Common.InitODataClient();

            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = parameters[shift + 4].Trim();
            var contractCategory = BusinessLogic.GetEntityWithFilter<IContractCategories>(c => c.Name == name, exceptionList, TestSettings.Logger, true);

            if (contractCategory == null)
            {
                contractCategory = new IContractCategories() { Name = name };
                BusinessLogic.CreateEntity(contractCategory, exceptionList, TestSettings.Logger);
            }

            return contractCategory;
        }
    }
}
