using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData;
using Xunit.Extensions.Ordering;

namespace Tests
{
    [Order(0)]
    public class Init
    {
        [Fact]
        public void T0_Init()
        {
            ChangeRegNumber(TestSettings.ContractsPathXlsx, Constants.SheetNames.Contracts);
            //ChangeRegNumber(TestSettings.AddendumsPathXlsx, Constants.SheetNames.Addendums);
            ChangeRegNumber(TestSettings.SupagreementsPathXlsx, Constants.SheetNames.SupAgreements);
            ChangeRegNumber(TestSettings.IncomingLettersPathXlsx, Constants.SheetNames.IncomingLetters);
            ChangeRegNumber(TestSettings.OutgoingLettersPathXlsx, Constants.SheetNames.OutgoingLetters);
            ChangeRegNumber(TestSettings.OrdersPathXlsx, Constants.SheetNames.Orders);
            ChangeRegNumber(TestSettings.CompanyDirectivesPathXlsx, Constants.SheetNames.CompanyDirectives);
        }

        internal static void ChangeRegNumber(string xlsxPath, string sheetName)
        {
            var logger = TestSettings.Logger;
            var excelProcessor = new ExcelProcessor(xlsxPath, sheetName, logger);

            var items = Common.XlsxParse(xlsxPath, sheetName);
            var listArrayParams = new List<ArrayList>();
            var newRegNumber = new Random(int.MaxValue);
            var title = excelProcessor.GetExcelColumnName(1);
            for (var i = 0; i < items.Count(); i++)
            {
                var arrayParams = new ArrayList { newRegNumber.Next(), title, i + 2 };
                listArrayParams.Add(arrayParams);
            }

            excelProcessor.InsertText(listArrayParams, listArrayParams.First().Count);
        }
    }
}
