using ImportData;

namespace Tests.Databooks
{
    public class Company
    {
        [Fact]
        public void EmployeesImport()
        {
            Program.Main(TestSettings.GetArgsCompany());

            var result = true;
            foreach (var expectedEmployee in TestData.Company.Employees.GetEmployees())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualEmployee = BusinessLogic.GetEntityWithFilter<IEmployees>(x => x.Name == expectedEmployee.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedEmployee == actualEmployee;
            }
            Assert.True(result);
        }
        [Fact]
        public void BusinessUnitsImport()
        {
            var result = true;
            foreach (var expectedBusinessUnit in TestData.Company.BusinessUnits.GetBusinessUnits())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualBusinessUnit = BusinessLogic.GetEntityWithFilter<IBusinessUnits>(x => x.Name == expectedBusinessUnit.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedBusinessUnit == actualBusinessUnit;
            }

            Assert.True(result);
        }

        [Fact]
        public void DepartmentsImport()
        {
            var result = true;
            foreach (var expectedDepartment in TestData.Company.Departments.GetDepartments())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualDepartment = BusinessLogic.GetEntityWithFilter<IDepartments>(x => x.Name == expectedDepartment.Name, exceptionList, TestSettings.Logger, true);

                result = result && expectedDepartment == actualDepartment;
            }
            Assert.True(result);
        }
    }
}