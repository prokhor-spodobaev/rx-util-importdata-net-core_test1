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
        public void CompaniesImport()
        {
            Program.Main(TestSettings.GetArgsCompanies());

            var result = true;
            foreach (var expectedCompany in TestData.Companies.GetCompanies())
            {
                var exceptionList = new List<Structures.ExceptionsStruct>();
                var actualCompany = BusinessLogic.GetEntityWithFilter<ICompanies>(x => x.Name == expectedCompany.Name &&
                                  x.TIN == expectedCompany.TIN &&
                                  x.TRRC == expectedCompany.TRRC &&
                                  x.PSRN == expectedCompany.PSRN, exceptionList, TestSettings.Logger, true);

                result = result && expectedCompany == actualCompany;
            }
            Assert.True(result);
        }
    }
}
