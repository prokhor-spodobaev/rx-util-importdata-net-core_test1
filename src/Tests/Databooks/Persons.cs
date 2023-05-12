using ImportData;

namespace Tests.Databooks
{
    public class Persons
    {
        [Fact]
        public void PersonsImport()
        {
            Program.Main(TestSettings.GetArgsPersons());

            var result = true;
            foreach (var expectedPerson in TestData.Persons.GetPersons())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualPerson = BusinessLogic.GetEntityWithFilter<IPersons>(x => x.Name == expectedPerson.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedPerson == actualPerson;
            }
            Assert.True(result);
        }
    }
}