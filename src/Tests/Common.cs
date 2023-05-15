using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData;
using ImportData.Entities.Databooks;

namespace Tests
{
    internal static class Common
    {
        /// <summary>
        /// Получить строку выполнения утилиты.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="xlsxPath">Путь к xlsx файлу.</param>
        /// <returns>Массив параметров.</returns>
        public static string[] GetArgs(string action, string xlsxPath) => new[] { "-n", TestSettings.Login, "-p", TestSettings.Password, "-a", action, "-f", xlsxPath };

        /// <summary>
        /// Инициализация OData клиента.
        /// </summary>
        public static void InitODataClient() => Program.Main(GetArgs(Constants.Actions.InitForTests, Constants.Actions.InitForTests));

        /// <summary>
        /// Парсинг xlsx файла.
        /// </summary>
        /// <param name="xlsxPath">Путь к файлу.</param>
        /// <param name="sheetName">Имя листа.</param>
        /// <param name="logger">Логгер.</param>
        /// <returns>Распарсенные строки.</returns>
        public static IEnumerable<List<string>> XlsxParse(string xlsxPath, string sheetName, NLog.Logger logger)
        {
            var excelProcessor = new ExcelProcessor(xlsxPath, sheetName, logger);
            return excelProcessor.GetDataFromExcel().Skip(1);
        }

        /// <summary>
        /// Преобразовать зачение в дату.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Преобразованная дата.</returns>
        /// <exception cref="FormatException" />
        public static DateTimeOffset ParseDate(string value)
        {
            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            var culture = CultureInfo.CreateSpecificCulture("en-GB");

            if (!string.IsNullOrEmpty(value))
            {
                DateTimeOffset date;
                if (DateTimeOffset.TryParse(value.Trim(), culture.DateTimeFormat, DateTimeStyles.AssumeUniversal, out date))
                    return date;

                var dateDouble = 0.0;
                if (double.TryParse(value.Trim(), style, culture, out dateDouble))
                    return new DateTimeOffset(DateTime.FromOADate(dateDouble), TimeSpan.Zero);

                throw new FormatException("Неверный формат строки.");
            }
            else
                return DateTimeOffset.MinValue;
        }

        #region Методы сравнения.
        /// <summary>
        /// Сравнить параметры и получить строку с ошибкой.
        /// </summary>
        /// <param name="actual">Актуальный (из системы).</param>
        /// <param name="expected">Ожидаемый (из xlsx).</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Строку с ошибкой, если параметры не равны.</returns>
        public static string CheckParam(string? actual, string? expected, string paramName) => actual == expected ? string.Empty : $"ParamName: {paramName}. Expected: {expected}. Actual: {actual}";

        /// <summary>
        /// Сравнить параметры и получить строку с ошибкой.
        /// </summary>
        /// <param name="actual">Актуальный (из системы).</param>
        /// <param name="expected">Ожидаемый (из xlsx).</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Строку с ошибкой, если параметры не равны.</returns>
        public static string CheckParam(DateTimeOffset actual, DateTimeOffset expected, string paramName) => actual == expected ? string.Empty : $"ParamName: {paramName}. Expected: {expected}. Actual: {actual}";

        /// <summary>
        /// Сравнить параметры и получить строку с ошибкой.
        /// </summary>
        /// <param name="actual">Актуальный (из системы).</param>
        /// <param name="expected">Ожидаемый (из xlsx).</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Строку с ошибкой, если параметры не равны.</returns>
        public static string CheckParam(int? actual, string? expected, string paramName) => actual.ToString() == expected ? string.Empty : $"ParamName: {paramName}. Expected: {expected}. Actual: {actual}";

        /// <summary>
        /// Сравнить параметры и получить строку с ошибкой.
        /// </summary>
        /// <param name="actual">Актуальный (из системы).</param>
        /// <param name="expected">Ожидаемый (из xlsx).</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Строку с ошибкой, если параметры не равны.</returns>
        public static string CheckParam(double? actual, string? expected, string paramName) => actual.ToString() == expected ? string.Empty : $"ParamName: {paramName}. Expected: {expected}. Actual: {actual}";

        /// <summary>
        /// Получить строку с ошибкой по результату сравнения.
        /// </summary>
        /// <param name="equals">Результат сравнения.</param>
        /// <param name="paramName">Имя параметра.</param>
        /// <returns>Строку с ошибкой, если параметры не равны.</returns>
        public static string CheckParam(bool equals, string paramName) => equals ? string.Empty : $"ParamName: {paramName}. Expected: true. Actual: false";
        #endregion
    }
}
