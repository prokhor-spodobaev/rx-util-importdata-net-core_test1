using ImportData;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void OrdersImport()
        {
            var xlsxPath = TestSettings.OrdersPathXlsx;
            var action = ImportData.Constants.Actions.ImportOrders;
            var sheetName = ImportData.Constants.SheetNames.Orders;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedOrder in items)
            {
                var error = EqualsOrders(expectedOrder);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsOrders(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var actualOrder = BusinessLogic.GetEntityWithFilter<IOrders>(x => true, exceptionList, TestSettings.Logger, true);

            if (actualOrder == null)
                return $"Не найдено дополнительное соглашение";

            var errorList = new List<string>
            {

            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности:");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}