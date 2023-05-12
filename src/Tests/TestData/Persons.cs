using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData
{
    internal class Persons
    {
        public static IEnumerable<IPersons> GetPersons() => new[] {
            new IPersons() {
                LastName = "Сергеев",
                FirstName = "Иван",
                MiddleName = "Алексеевич",
                Name = "Сергеев Иван Алексеевич",
                ShortName = null,
                Sex = "",
                DateOfBirth = null,
                TIN = "",
                INILA = "",
                City = null,
                Region = null,
                LegalAddress = "",
                PostalAddress = "",
                Phones = "",
                Email = "",
                Homepage = "",
                Bank = null,
                Account  = "",
                Note = "Для ЭДО",

                Nonresident = false,
                PSRN = null,
                NCEO = null,
                NCEA = null,
                CanExchange = null,
                Code = "",
                Status = "",
                Responsible = null
            },
            new IPersons() {
                LastName = "Сергеев",
                FirstName = "Иван",
                MiddleName = "Алексеевич",
                Name = "Сергеев Иван Алексеевич",
                ShortName = null,
                Sex = "",
                DateOfBirth = null,
                TIN = "",
                INILA = "",
                City = null,
                Region = null,
                LegalAddress = "",
                PostalAddress = "",
                Phones = "",
                Email = "",
                Homepage = "",
                Bank = null,
                Account  = "",
                Note = "Для ЭДО",

                Nonresident = false,
                PSRN = null,
                NCEO = null,
                NCEA = null,
                CanExchange = null,
                Code = "",
                Status = "",
                Responsible = null
            },
        };
    }
}
