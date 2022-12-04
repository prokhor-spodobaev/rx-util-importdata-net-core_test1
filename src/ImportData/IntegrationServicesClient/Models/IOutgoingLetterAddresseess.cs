namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Список рассылки исходящего письма")]
  class IOutgoingLetterAddresseess
  {
    public ICounterparties Correspondent { get; set; }
    public IContacts Addressee { get; set; }
    public IMailDeliveryMethods DeliveryMethod { get; set; }
    public IOutgoingLetters OutgoingDocumentBase { get; set; }
  }
}
