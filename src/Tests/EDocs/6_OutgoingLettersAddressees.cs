using ImportData;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void OutgoingLettersAddresseesImport()
        {
            var xlsxPath = TestSettings.OutgoingLettersAddresseesPathXlsx;
            var action = ImportData.Constants.Actions.ImportOutgoingLettersAddressees;
            var sheetName = ImportData.Constants.SheetNames.OutgoingLettersAddressees;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

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
            var regNumber = parameters[shift + 0].Trim();

            DateTimeOffset regDate = DateTimeOffset.MinValue;

            var regDateBeginningOfDay = Common.ParseDate(parameters[shift + 1].Trim());
            var counterpartyName = parameters[shift + 2].Trim();
            var documentRegisterId = -1;
            int.TryParse(parameters[shift + 17].Trim(), out documentRegisterId);


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