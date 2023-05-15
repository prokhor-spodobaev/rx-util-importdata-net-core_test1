using ImportData;

namespace Tests.EDocs
{
    public partial class Tests
    {
        [Fact]
        public void CompanyDirectivesImport()
        {
            var xlsxPath = TestSettings.CompanyDirectivesPathXlsx;
            var action = ImportData.Constants.Actions.ImportCompanyDirectives;
            var sheetName = ImportData.Constants.SheetNames.CompanyDirectives;
            var logger = TestSettings.Logger;
            var items = Common.XlsxParse(xlsxPath, sheetName, logger);

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedCompanyDirective in items)
            {
                var error = EqualsCompanyDirectives(expectedCompanyDirective);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsCompanyDirectives(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var actualCompanyDirective = BusinessLogic.GetEntityWithFilter<ICompanyDirective>(x => true, exceptionList, TestSettings.Logger, true);

            if (actualCompanyDirective == null)
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