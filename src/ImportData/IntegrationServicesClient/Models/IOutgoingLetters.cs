using System;

using NLog;

namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Исходящее письмо")]
  class IOutgoingLetters : IOutgoingDocumentBases
  {
    public void CreateAddressee(IOutgoingLetterAddresseess addressee, Logger logger)
    {
      try
      {
        if (!IsManyAddressees)
          IsManyAddressees = true;

        var result = Client.Instance().For<IOutgoingLetters>()
         .Key(this)
         .NavigateTo(nameof(Addressees))
         .Set(new IOutgoingLetterAddresseess()
         {
           Addressee = addressee.Addressee,
           DeliveryMethod = addressee.DeliveryMethod,
           Correspondent = addressee.Correspondent,
           OutgoingDocumentBase = this,
         })
         .InsertEntryAsync().Result;
      }
      catch (Exception ex)
      {
        logger.Error(ex);
        throw;
      }
    }
  }
}
