using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData.Company
{
    internal class BusinessUnits
    {
        public static IEnumerable<IBusinessUnits> GetBusinessUnits() => new[] {
            new IBusinessUnits() {
                Name = "ТехноСистемы ЗАО",
                LegalName = "Закрытое Акционерное Общество \"ТехноСистемы\"",
                HeadCompany = null,
                CEO = new IEmployees() { Name = "Ивановский Геннадий Александрович" },
                Nonresident = false,
                TIN = "1834101061",
                TRRC = "183400001",
                PSRN = "",
                NCEO = "",
                NCEA  = "",
                City = new ICities() { Name = "г. Ижевск"},
                Region = new IRegions() { Name = "Удмуртская Республика" },
                LegalAddress = "ул. Воткинское шоссе, 86а, корп. 2,  г. Ижевск, УР",
                PostalAddress = "ул. Воткинское шоссе, 86а, корп. 2,  г. Ижевск, УР",
                Phones = "45-78-78",
                Email  = "info@tech-sys.ru",
                Homepage  = "http://www.npo-comp.ru/",
                Note  = "",
                Account = "",
                Bank = null,

                Description = null,
                Code = null
            },
            new IBusinessUnits() {
                Name = "Смарт-Сервис, ООО",
                LegalName = "Общество с ограниченной ответственностью \"Смарт-Сервис\"",
                HeadCompany = new IBusinessUnits() { Name = "ТехноСистемы ЗАО" },
                CEO = new IEmployees() { Name = "Ивановский Геннадий Александрович" },
                Nonresident = false,
                TIN = "9640773703",
                TRRC = "964001000",
                PSRN = "1077604011934",
                NCEO = "",
                NCEA  = "",
                City = new ICities() { Name = "г. Ижевск"},
                Region = new IRegions() { Name = "Удмуртская Республика" },
                LegalAddress = "426033, Удмуртская Республика, Ижевск, пер. Северный, 61",
                PostalAddress = "426033, Удмуртская Республика, Ижевск, пер. Северный, 61-606",
                Phones = "",
                Email  = "",
                Homepage  = "",
                Note  = "участник ЭДО",
                Account = "",
                Bank = null,

                Description = null,
                Code = null
            },
        };
    }
}
