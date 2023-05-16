using ImportData;
using Xunit.Extensions.Ordering;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact, Order(60)]
        public void T6_OutgoingLettersAddresseesImport()
        {
            var xlsxPath = TestSettings.OutgoingLettersAddresseesPathXlsx;
            var action = ImportData.Constants.Actions.ImportOutgoingLettersAddressees;
            var sheetName = ImportData.Constants.SheetNames.OutgoingLettersAddressees;

            var items = Common.XlsxParse(xlsxPath, sheetName);

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedOutgoingLettersAddressees in items)
            {
                var error = EqualsOutgoingLettersAddressees(expectedOutgoingLettersAddressees);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsOutgoingLettersAddressees(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var actualOutgoingLettersAddressees = BusinessLogic.GetEntityWithFilter<IOutgoingLetterAddresseess>(x => true, exceptionList, TestSettings.Logger, true);

            if (actualOutgoingLettersAddressees == null)
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