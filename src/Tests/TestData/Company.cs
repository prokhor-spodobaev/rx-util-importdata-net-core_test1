using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData
{
    internal static class Company
    {
    public static IEnumerable<ICompanies> GetCompanies() => new[] { 
      new ICompanies() {
                Name = "Авалон ООО",
                LegalName = "ООО «АВАЛОН»",
                HeadCompany = null,
                Nonresident = false,
                TIN = "",
                PSRN = "",
                NCEO = "",
                NCEA = "",
                TRRC = "",
                City = new ICities() { Name = "г. Химки" },
                Region = null,
                LegalAddress = "141407, Московская область, г. Химки, ул. Лавочкина, д. 13, корпус 2",
                PostalAddress = "141707, Московская область, г. Долгопрудный, Лихачевский проезд, д. 19, оф. 315",
                Phones = "+7 962 944 44 46",
                Email = "Игорь Паламарчук <i_palamarchuk@list.ru>",
                Homepage = "",
                Note = "",
                Account = "",
                Bank = null,
                Responsible = null
                },
          new ICompanies() {
                Name = "",
                LegalName = string.Empty,
                HeadCompany = null,
                Nonresident = false,
                TIN = "",
                PSRN = "",
                NCEO = "",
                NCEA = "",
                TRRC = string.Empty,
                City = new ICities(),
                Region = new IRegions(),
                LegalAddress = "",
                PostalAddress = "",
                Phones = "",
                Email = "",
                Homepage = "",
                Note = "",
                Account = "",
                Bank = new IBanks(),
                Responsible = new IEmployees()
                },
          new ICompanies() {
                Name = "",
                LegalName = string.Empty,
                HeadCompany = null,
                Nonresident = false,
                TIN = "",
                PSRN = "",
                NCEO = "",
                NCEA = "",
                TRRC = string.Empty,
                City = new ICities(),
                Region = new IRegions(),
                LegalAddress = "",
                PostalAddress = "",
                Phones = "",
                Email = "",
                Homepage = "",
                Note = "",
                Account = "",
                Bank = new IBanks(),
                Responsible = new IEmployees()
                },
          new ICompanies() {
                Name = "",
                LegalName = string.Empty,
                HeadCompany = null,
                Nonresident = false,
                TIN = "",
                PSRN = "",
                NCEO = "",
                NCEA = "",
                TRRC = string.Empty,
                City = new ICities(),
                Region = new IRegions(),
                LegalAddress = "",
                PostalAddress = "",
                Phones = "",
                Email = "",
                Homepage = "",
                Note = "",
                Account = "",
                Bank = new IBanks(),
                Responsible = new IEmployees()
                }};
  }
}
