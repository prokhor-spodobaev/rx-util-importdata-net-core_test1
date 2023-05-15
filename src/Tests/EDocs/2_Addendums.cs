using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void AddendumsImport()
        {
            var xlsxPath = TestSettings.AddendumsPathXlsx;
            var action = ImportData.Constants.Actions.ImportAddendums;
            var sheetName = ImportData.Constants.SheetNames.Addendums;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

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
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var actualAddendum = BusinessLogic.GetEntityWithFilter<IIncomingLetters>(x => true, exceptionList, TestSettings.Logger, true);

            if (actualAddendum == null)
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
