namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Организация")]
    public class ICompanies : ICounterparties
    {
        public string TRRC { get; set; }
        public bool IsCardReadOnly { get; set; }
        public string LegalName { get; set; }
        public ICompanies HeadCompany { get; set; }

        public static ICompanies CastCounterpartyToCompany(ICounterparties counterparty)
        {
            var company = new ICompanies
            {
                Name = counterparty.Name,
                TIN = counterparty.TIN,
                LegalAddress = counterparty.LegalAddress,
                PostalAddress = counterparty.PostalAddress,
                Phones = counterparty.Phones,
                Email = counterparty.Email,
                Homepage = counterparty.Homepage,
                Note = counterparty.Note,
                Nonresident = counterparty.Nonresident,
                PSRN = counterparty.PSRN,
                NCEO = counterparty.NCEO,
                NCEA = counterparty.NCEA,
                Account = counterparty.Account,
                CanExchange = counterparty.CanExchange,
                Code = counterparty.Code,
                Status = counterparty.Status,
                Id = counterparty.Id,
                City = counterparty.City,
                Region = counterparty.Region,
                Bank = counterparty.Bank,
                Responsible = counterparty.Responsible,

                TRRC = string.Empty,
                IsCardReadOnly = false,
                LegalName = string.Empty,
                HeadCompany = null,
            };

            return company;
        }
    }
}
