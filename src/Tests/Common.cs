using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData;
using ImportData.Entities.Databooks;

namespace Tests
{
    internal static class Common
    {
        public static string[] GetArgs(string action, string xlsxPath) => new[] { "-n", TestSettings.Login, "-p", TestSettings.Password, "-a", action, "-f", xlsxPath };
        public static string CheckParam(string? actual, string? expected, string paramName) => expected == actual ? string.Empty : $"ParamName: {paramName}. Expected: {expected}. Actual: {actual}";
        public static IEnumerable<List<string>> XlsxParse(string xlsxPath, string sheetName, NLog.Logger logger)
        {
            var excelProcessor = new ExcelProcessor(xlsxPath, sheetName, logger);
            return excelProcessor.GetDataFromExcel().Skip(1);
        }
    }
}
