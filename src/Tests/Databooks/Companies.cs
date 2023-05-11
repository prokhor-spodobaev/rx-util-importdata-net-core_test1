using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ImportData;
using ImportData.IntegrationServicesClient.Models;

namespace Tests.Databooks
{
    public class Companies
    {
        [Fact]
        public void CompanyNotNull()
        {
            Program.Main(TestSettings.GetArgsCompanies());

            foreach (var expectedCompany in TestData.Company.GetCompanies())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualCompany = BusinessLogic.GetEntityWithFilter<ICompanies>(x => x.Name == expectedCompany.Name &&
                                  x.TIN == expectedCompany.TIN &&
                                  x.TRRC == expectedCompany.TRRC &&
                                  x.PSRN == expectedCompany.PSRN, exceptionList, TestSettings.Logger, true);

                Assert.True(expectedCompany.Equals(actualCompany));
            }
        }
    }
}
