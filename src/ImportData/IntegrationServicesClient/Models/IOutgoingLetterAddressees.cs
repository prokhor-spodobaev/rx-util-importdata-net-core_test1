using System;

namespace ImportData.IntegrationServicesClient.Models
{
  public class IOutgoingLetterAddressees
  {
    //public int Id { get; set; }
    public ICounterparties Correspondent { get; set; }
    public IContacts Addressee { get; set; }
    public string DeliveryMethod { get; set; }
    public int Number { get; set; }
  }
}
