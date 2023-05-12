using ImportData;

namespace Tests.Databooks
{
  public class Contacts
  {
        [Fact]
        public void ContactImport()
        {
            Program.Main(TestSettings.GetArgsContacts());

            var result = true;
            foreach (var expectedContact in TestData.Contacts.GetContacts())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualContact = BusinessLogic.GetEntityWithFilter<IContacts>(x => x.Name == expectedContact.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedContact == actualContact;
            }
            Assert.True(result);
        }
    }
}