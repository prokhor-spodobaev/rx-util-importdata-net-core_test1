using ImportData;

namespace Tests.Databooks
{
    public partial class Tests
    {
        [Fact]
        public void T4_LoginsImport()
        {
            var xlsxPath = TestSettings.LoginsPathXlsx;
            var action = ImportData.Constants.Actions.ImportLogins;
            var sheetName = ImportData.Constants.SheetNames.Logins;
            var logger = TestSettings.Logger;

            Program.Main(Common.GetArgs(action, xlsxPath));

            var errorList = new List<string>();
            foreach (var expectedLogin in Common.XlsxParse(xlsxPath, sheetName, logger))
            {
                var error = EqualsLogin(expectedLogin);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsLogin(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = string.Join(' ', parameters[shift + 1].Trim(), parameters[shift + 2].Trim(), parameters[shift + 3].Trim()).Trim();
            var actualEmployee = BusinessLogic.GetEntityWithFilter<IEmployees>(x => x.Name == name, exceptionList, TestSettings.Logger, true);
            var actualLogin = actualEmployee?.Login;
            var actualPerson = actualEmployee?.Person;

            if (actualEmployee == null)
                return $"Не найден сотрудник {name}";
            if (actualEmployee == null)
                return $"Не найден логин {name}";
            if (actualPerson == null)
                return $"Не найдена персона {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualLogin == null ? string.Empty : actualLogin.LoginName, parameters[shift + 0].Trim(), "LoginName"),
                Common.CheckParam(actualPerson == null ? string.Empty : actualPerson.LastName, parameters[shift + 1].Trim(), "LastName"),
                Common.CheckParam(actualPerson == null ? string.Empty : actualPerson.FirstName, parameters[shift + 2].Trim(), "FirstName"),
                Common.CheckParam(actualPerson == null ? string.Empty : actualPerson.MiddleName, parameters[shift + 3].Trim(), "MiddleName")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}