using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Tests
{
  internal static class TestSettings
  {
    public static Logger Logger = LogManager.GetCurrentClassLogger();

    public const string Login = "Administrator";
    public const string Password = "11111";
    public const string XlsxPath = "Templates";

    public const string Company = "Company.xlsx";
    public const string Companies = "Companies.xlsx";
    public const string Persons = "Persons.xlsx";
    public const string Logins = "Logins.xlsx";
    public const string Contacts = "Contacts.xlsx";
    public const string Supagreements = "Supagreements.xlsx";
    public const string IncomingLetters = "IncomingLetters.xlsx";
    public const string OutgoingLettersAddressees = "OutgoingLettersAddressees.xlsx";
    public const string Orders = "Orders.xlsx";
    public const string CompanyDirectives = "IncomingLetters.xlsx";

    public static string[] GetArgsCompany() => new[] { "-n", Login, "-p", Password, "-a", "ImportCompany", "-f", $@"{XlsxPath}\{Company}" };
    public static string[] GetArgsCompanies() => new[] { "-n", Login, "-p", Password, "-a", "ImportCompanies", "-f", $@"{XlsxPath}\{Companies}" };
    public static string[] GetArgsPersons() => new[] { "-n", Login, "-p", Password, "-a", "ImportPersons", "-f", $@"{XlsxPath}\{Persons}" };
    public static string[] GetArgsLogins() => new[] { "-n", Login, "-p", Password, "-a", "ImportLogins", "-f", $@"{XlsxPath}\{Logins}" };
    public static string[] GetArgsContacts() => new[] { "-n", Login, "-p", Password, "-a", "ImportContacts", "-f", $@"{XlsxPath}\{Contacts}" };
    public static string[] GetArgsSupagreements() => new[] { "-n", Login, "-p", Password, "-a", "ImportSupagreements", "-f", $@"{XlsxPath}\{Supagreements}" };
    public static string[] GetArgsIncomingLetters() => new[] { "-n", Login, "-p", Password, "-a", "ImportIncomingLetters", "-f", $@"{XlsxPath}\{IncomingLetters}" };
    public static string[] GetArgsOutgoingLettersAddressees() => new[] { "-n", Login, "-p", Password, "-a", "ImportOutgoingLettersAddressees", "-f", $@"{XlsxPath}\{OutgoingLettersAddressees}" };
    public static string[] GetArgsOrders() => new[] { "-n", Login, "-p", Password, "-a", "ImportOrders", "-f", $@"{XlsxPath}\{Orders}" };
    public static string[] GetArgsCompanyDirectives() => new[] { "-n", Login, "-p", Password, "-a", "ImportCompanyDirectives", "-f", $@"{XlsxPath}\{CompanyDirectives}" };
  }
}
