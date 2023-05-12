using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData.Company
{
    internal class Departments
    {
        public static IEnumerable<IDepartments> GetDepartments() => new[] {
            new IDepartments() {
                Name = "Финансово-юридический отдел",
                ShortName = "",
                Code = "ФЮО",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО"},
                HeadOffice = null,
                Manager = new IEmployees(){ Name = "Законов Сергей Юрьевич" },
                Phone = "",
                Note = "",
                Description = null,
            },
            new IDepartments() {
                Name = "Технический отдел",
                ShortName = "",
                Code = "ТО",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО"},
                HeadOffice = null,
                Manager = new IEmployees(){ Name = "Коломенцев Сергей Петрович" },
                Phone = "",
                Note = "",
                Description = null,
            },
            new IDepartments() {
                Name = "Служба генерального директора",
                ShortName = "",
                Code = "СГД",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО"},
                HeadOffice = null,
                Manager = new IEmployees(){ Name = "Ивановский Геннадий Александрович" },
                Phone = "",
                Note = "",
                Description = null,
            },
            new IDepartments() {
                Name = "Отдел снабжения",
                ShortName = "",
                Code = "ОС",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО"},
                HeadOffice = null,
                Manager = new IEmployees(){ Name = "Иванов Иван Иванович" },
                Phone = "",
                Note = "",
                Description = null,
            },
            new IDepartments() {
                Name = "Отдел продаж",
                ShortName = "",
                Code = "",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО"},
                HeadOffice = null,
                Manager = null,
                Phone = "",
                Note = "",
                Description = null,
            },
        }; 
    }
}
