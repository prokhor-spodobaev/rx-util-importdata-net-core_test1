using System.Linq;

using NLog;

namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Исходящее письмо")]
  class IOutgoingLetters : IOutgoingDocumentBase
  {
    public IOutgoingLetterAddressees CreateAddressee(IContacts contact, ICounterparties correspondent, IMailDeliveryMethods deliveryMethod, Logger logger)
    {
      logger.Debug("IsManyAddressees");
      if (!this.IsManyAddressees)
      {
        this.IsManyAddressees = true;
        logger.Debug("IsManyAddressees = true;");
      }

      var addressee = Client.Instance().For<IOutgoingLetters>()
      .Key(this.Id)
      .NavigateTo(x => x.Addressees)
      .Set(new IOutgoingLetterAddressees
      {
        Addressee = contact,
        Correspondent = correspondent,
        DeliveryMethod = deliveryMethod,
        OutgoingDocumentBase = this
      })
      .InsertEntryAsync()
      .Result
      .LastOrDefault();

      return addressee;
    }
  }
}
