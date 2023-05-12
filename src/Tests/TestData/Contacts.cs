using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData
{
    internal class Contacts
    {
        public static IEnumerable<IContacts> GetContacts() => new[] {
            new IContacts() {
                Person = new IPersons() { Name = "Иванов Иван Иванович", LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович" },
                Company = new ICompanies() { Name = "Авалон ООО" },
                Name = "Иванов Иван Иванович",
                JobTitle  = "Главный бухгалтер",
                Phone = "",
                Fax = "",
                Email = "",
                Homepage  = "",
                Note = ""
            },
            new IContacts() {
                Person = new IPersons() { Name = "Петров Петр Петрович", LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович" },
                Company = new ICompanies() { Name = "Политекс ООО" },
                Name = "Петров Петр Петрович",
                JobTitle  = "Инженер",
                Phone = "",
                Fax = "",
                Email = "",
                Homepage  = "",
                Note = ""
            },
            new IContacts() {
                Person = new IPersons() { Name = "Владимиров Владимир Владимирович", LastName = "Владимиров", FirstName = "Владимир", MiddleName = "Владимирович" },
                Company = new ICompanies() { Name = "Точка Росы ООО" },
                Name = "Владимиров Владимир Владимирович",
                JobTitle  = "Главный специалист",
                Phone = "",
                Fax = "",
                Email = "",
                Homepage  = "",
                Note = ""
            }
        };
    }
}
