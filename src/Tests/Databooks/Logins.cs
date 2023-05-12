using ImportData;

namespace Tests.Databooks
{
  public class Logins
  {
        [Fact]
        public void LoginsImport()
        {
            Program.Main(TestSettings.GetArgsLogins());

            var result = true;
            foreach (var expectedLogin in TestData.Logins.GetLogins())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualLogin = BusinessLogic.GetEntityWithFilter<IEmployees>(x => x.Name == expectedLogin.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedLogin.Login.LoginName == actualLogin.Login.LoginName;
            }
            Assert.True(result);
        }
    }
}