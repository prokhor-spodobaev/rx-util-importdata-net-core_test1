namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Адресаты исходящего письма")]
  class IOutgoingLetterAddressees
  {
    public int OutgoingLetterId { get; set; }
    public ICounterparties Correspondent { get; set; }
    public IContacts Addressee { get; set; }
    public IMailDeliveryMethods DeliveryMethod { get; set; }
    public int Number { get; set; }
  }
}
