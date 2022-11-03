namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Адресаты исходящего письма")]
  class IOutgoingLetterAddresseess
  {
    public ICounterparties Correspondent { get; set; }
    public IContacts Addressee { get; set; }
    public IMailDeliveryMethods DeliveryMethod { get; set; }
    public IOutgoingDocumentBases OutgoingDocumentBase { get; set; }
  }
}
