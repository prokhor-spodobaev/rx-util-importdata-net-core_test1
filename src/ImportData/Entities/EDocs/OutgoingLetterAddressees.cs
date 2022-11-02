using System;
using System.Collections.Generic;
using System.Globalization;
using NLog;
using ImportData.IntegrationServicesClient.Models;
using System.IO;

namespace ImportData
{
  class OutgoingLetterAddressees : Entity
  {
    public int PropertiesCount = 15;
    /// <summary>
    /// Получить наименование число запрашиваемых параметров.
    /// </summary>
    /// <returns>Число запрашиваемых параметров.</returns>
    public override int GetPropertiesCount()
    {
      return PropertiesCount;
    }

    /// <summary>
    /// Сохранение сущности в RX.
    /// </summary>
    /// <param name="shift">Сдвиг по горизонтали в XLSX документе. Необходим для обработки документов, составленных из элементов разных сущностей.</param>
    /// <param name="logger">Логировщик.</param>
    /// <returns>Число запрашиваемых параметров.</returns>
    public override IEnumerable<Structures.ExceptionsStruct> SaveToRX(Logger logger, bool supplementEntity, string ignoreDuplicates, int shift = 0)
    {
      var exceptionList = new List<Structures.ExceptionsStruct>();
      var variableForParameters = this.Parameters[shift + 0].Trim();
      var culture = CultureInfo.CreateSpecificCulture("en-GB");

      var documentId = 0;
      try
      {
        documentId = int.Parse(variableForParameters);
      }
      catch (Exception)
      {
        var message = string.Format("Не удалось обработать Id документа \"{0}\".", this.Parameters[shift + 0].Trim());
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }

      var outgoingLetter = BusinessLogic.GetEntityWithFilter<IOutgoingLetters>(d => d.Id == documentId, exceptionList, logger);
      if (outgoingLetter == null)
      {
        var message = string.Format("Не найдено исходящее письмо с Id \"{0}\".", documentId);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }

      variableForParameters = this.Parameters[shift + 1].Trim();
      var counterparty = BusinessLogic.GetEntityWithFilter<ICounterparties>(c => c.Name == variableForParameters, exceptionList, logger);

      if (counterparty == null)
      {
        var message = string.Format("Не найден контрагент \"{0}\".", this.Parameters[shift + 1]);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }

      variableForParameters = this.Parameters[shift + 2].Trim();
      var contact = BusinessLogic.GetEntityWithFilter<IContacts>(d => d.Name == variableForParameters, exceptionList, logger);

      if (contact == null)
      {
        var message = string.Format("Не найдено подразделение \"{0}\".", variableForParameters);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Warn(message);
      }

      variableForParameters = this.Parameters[shift + 3].Trim();
      var deliveryMethod = BusinessLogic.GetEntityWithFilter<IMailDeliveryMethods>(m => m.Name == variableForParameters, exceptionList, logger);

      if (!string.IsNullOrEmpty(variableForParameters) && deliveryMethod == null)
      {
        var message = string.Format("Не найден Способ доставки \"{1}\". Исходящее письмо с Id: \"{0}\". ", documentId, variableForParameters);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
        logger.Warn(message);
      }

      var number = 0;
      variableForParameters = this.Parameters[shift + 4].Trim();
      try
      {
        number = int.Parse(variableForParameters);
      }
      catch (Exception)
      {
        var message = string.Format("Не удалось обработать номер отправки \"{0}\".", this.Parameters[shift + 4]);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }

      try
      {
        var addressee = new IOutgoingLetterAddressees
        {
          Addressee = contact,
          Correspondent = counterparty,
          DeliveryMethod = deliveryMethod,
          Number = number
        };

        outgoingLetter.Addressees.Add(addressee);
        BusinessLogic.UpdateEntity<IOutgoingLetters>(outgoingLetter, exceptionList, logger);
      }
      catch (Exception ex)
      {
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = ex.Message });

        return exceptionList;
      }

      return exceptionList;
    }
  }
}
