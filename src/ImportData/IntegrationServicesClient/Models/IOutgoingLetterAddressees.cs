namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Адресаты исходящего письма")]
  class IOutgoingLetterAddressees
  {
    public ICounterparties Correspondent { get; set; }
    public IContacts Addressee { get; set; }
    public IMailDeliveryMethods DeliveryMethod { get; set; }
    public IOutgoingDocumentBase OutgoingDocumentBase { get; set; }
  }
}
