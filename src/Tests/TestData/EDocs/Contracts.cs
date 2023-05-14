using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestData.EDocs
{
    internal class Contracts
    {
       /* public static IEnumerable<IContracts> GetContracts() => new[] {
            new IContracts() {
                RegistrationNumber = "1",
                RegistrationDate = DateTimeOffset.Parse("20.08.2010"),
                Counterparty = new ICounterparties() { Name = "Digital River GmbH" },
                DocumentKind = new IDocumentKinds() { Name = "Договор" },
                DocumentGroup = new IDocumentGroupBases() { Name = "Сервисное обслуживание" },
                Subject = "Диагностика",
                BusinessUnit = new IBusinessUnits() { Name = "ТехноСистемы ЗАО" },
                Department = new IDepartments() { Name = "Отдел продаж" },
                ValidFrom = DateTimeOffset.Parse("20.08.2010"),
                ValidTill = DateTimeOffset.Parse("19.08.2015"),
                TotalAmount = 10000,
                Currency = new ICurrency() { Name = "Евро" },
                LifeCycleState = "Active",
                ResponsibleEmployee = null,
                OurSignatory = null,
                Note = "Оплачено",
                DocumentRegister = new IDocumentRegisters() { Id = 3 },
                RegistrationState = "Registered"

              /*Name = "",
                Assignee  = "",
                ResponsibleForReturnEmployee = "",
                DocumentDate = "",
                LeadingDocument = "",
                DeliveredTo = "",
                PreparedBy = "" 
            }, };
        */
    }
}
