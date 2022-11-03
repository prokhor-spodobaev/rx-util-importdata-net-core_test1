using System.Collections.Generic;

namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Исходящий документ")]
  class IOutgoingDocumentBase : IOfficialDocuments
  {
    public int Id { get; set; }
    public bool IsManyAddressees { get; set; }
    public IEmployees Addressee { get; set; }
    public IEmployees Assignee { get; set; }
    public IBusinessUnits BusinessUnit { get; set; }
    public ICounterparties Correspondent { get; set; }
    public IEmployees ResponsibleForReturnEmployee { get; set; }
    public IMailDeliveryMethods DeliveryMethod { get; set; }
    public List<IOutgoingLetterAddressees> Addressees { get; set; }
  }
}
