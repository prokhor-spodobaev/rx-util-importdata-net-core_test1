using ImportData;

namespace Tests.Databooks
{
    public partial class Tests
    {
        [Fact]
        public void T5_ContactsImport()
        {
            var xlsxPath = TestSettings.ContactsPathXlsx;
            var sheetName = ImportData.Constants.SheetNames.Contact;
            var logger = TestSettings.Logger;

            Program.Main(Common.GetArgs(TestSettings.ContactsAction, xlsxPath));

            var errorList = new List<string>();
            //Проверка работников.
            foreach (var expectedContact in Common.XlsxParse(xlsxPath, sheetName, logger))
            {
                var error = EqualsContact(expectedContact);

                if (string.IsNullOrEmpty(error))
                    continue;

                errorList.Add(error);
            }
            if (errorList.Any())
                Assert.Fail(string.Join(Environment.NewLine + Environment.NewLine, errorList));
        }

        public static string EqualsContact(List<string> parameters, int shift = 0)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();
            var name = string.Join(' ', parameters[shift + 0].Trim(), parameters[shift + 1].Trim(), parameters[shift + 2].Trim()).Trim();
            var actualContact = BusinessLogic.GetEntityWithFilter<IContacts>(x => x.Name == name, exceptionList, TestSettings.Logger, true);
            var actualPerson = actualContact.Person;

            if (actualContact == null)
                return $"Не найден контакт {name}";
            if (actualPerson == null)
                return $"Не найдена персона {name}";

            var errorList = new List<string>
            {
                Common.CheckParam(actualPerson.LastName, parameters[shift + 0].Trim(), "LastName"),
                Common.CheckParam(actualPerson.FirstName, parameters[shift + 1].Trim(), "FirstName"),
                Common.CheckParam(actualPerson.MiddleName, parameters[shift + 2].Trim(), "MiddleName"),
                Common.CheckParam(actualContact.Company == null ? string.Empty : actualContact.Company.Name, parameters[shift + 3].Trim(), "Company"),
                Common.CheckParam(actualContact.JobTitle, parameters[shift + 4].Trim(), "JobTitle"),
                Common.CheckParam(actualContact.Phone, parameters[shift + 5].Trim(), "Phone"),
                Common.CheckParam(actualContact.Fax, parameters[shift + 6].Trim(), "Fax"),
                Common.CheckParam(actualContact.Email, parameters[shift + 7].Trim(), "Email"),
                Common.CheckParam(actualContact.Homepage, parameters[shift + 8].Trim(), "Homepage"),
                Common.CheckParam(actualContact.Note, parameters[shift + 9].Trim(), "Note")
            };

            errorList = errorList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (errorList.Any())
                errorList.Insert(0, $"Ошибка в сущности: {name}");

            return string.Join(Environment.NewLine, errorList);
        }
    }
}