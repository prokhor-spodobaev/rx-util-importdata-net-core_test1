using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData
{
    internal class Logins
    {
        public static IEnumerable<IEmployees> GetLogins() => new[] {
            new IEmployees() {
                Name = "Иванов Иван Иванович",
                Person = new IPersons() { Name = "Иванов Иван Иванович", LastName = "Иванов", FirstName = "Сергей", MiddleName = "Иванович" },
                Login = new ILogins() { LoginName = "ivanov_s" }
            } 
        };
    }
}
