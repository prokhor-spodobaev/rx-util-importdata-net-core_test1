using System;
using System.Collections.Generic;


namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Право подписи")]
    public class ISignatureSettings : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public IRecipients Recipient { get; set; }
        public string Reason { get; set; }
        public IOfficialDocuments Document { get; set; }

        public IEnumerable<ISignatureSettingBusinessUnitss> BusinessUnits { get; set; }

        public IEnumerable<ISignatureSettingDocumentKindss> DocumentKinds { get; set; }

        public IEnumerable<ISignatureSettingDepartmentss> Departments { get; set; }

        public IEnumerable<ISignatureSettingCategoriess> Categories { get; set; }


        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }
        public string Limit { get; set; }
        public ICurrencies Currency { get; set; }
        public string Note { get; set; }
        public string DocumentInfo { get; set; }
        public string DocumentFlow { get; set; }
        public int? Amount { get; set; }
        public int Priority { get; set; }
        public ICertificates Certificate { get; set; }
        public string SigningReason { get; set; }
        public IJobTitles JobTitle { get; set; }

        public bool IsSystem { get; set; }
        public bool IsSystemCustomSC { get; set; }
        public string Status { get; set; }

    }
}
