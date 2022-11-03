using System;
using System.Collections.Generic;
using System.Globalization;
using NLog;
using ImportData.IntegrationServicesClient.Models;
using System.Linq;

namespace ImportData
{
  class OutgoingLetterAddressees : Entity
  {
    public int PropertiesCount = 4;
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

      logger.Debug("Получаем Id исходящего документа.");

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

      logger.Debug(string.Format("Id исходящего документа {0}.", documentId));
      logger.Debug("Получаем исходящий документ.");

      var outgoingLetter = BusinessLogic.GetEntityWithFilter<IOutgoingLetters>(d => d.Id == documentId, exceptionList, logger);
      if (outgoingLetter == null)
      {
        var message = string.Format("Не найдено исходящее письмо с Id \"{0}\".", documentId);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }
      logger.Debug(string.Format("Исходящий документ {0}.", outgoingLetter.Name));
      logger.Debug("Получаем контрагента.");

      variableForParameters = this.Parameters[shift + 1].Trim();

      var counterparty = BusinessLogic.GetEntityWithFilter<ICounterparties>(c => c.Name == variableForParameters, exceptionList, logger);

      if (counterparty == null)
      {
        var message = string.Format("Не найден контрагент \"{0}\".", this.Parameters[shift + 1]);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Error(message);

        return exceptionList;
      }

      logger.Debug(string.Format("Контрагент Id {0}.", counterparty.Id));

      variableForParameters = this.Parameters[shift + 2].Trim();
      var contact = !string.IsNullOrEmpty(variableForParameters)
        ? BusinessLogic.GetEntityWithFilter<IContacts>(d => d.Name == variableForParameters, exceptionList, logger)
        : null;

      if (!string.IsNullOrEmpty(variableForParameters) && contact == null)
      {
        var message = string.Format("Не найдено адресат \"{0}\".", variableForParameters);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
        logger.Warn(message);
      }

      variableForParameters = this.Parameters[shift + 3].Trim();
      var deliveryMethod = !string.IsNullOrEmpty(variableForParameters)
        ? BusinessLogic.GetEntityWithFilter<IMailDeliveryMethods>(m => m.Name == variableForParameters, exceptionList, logger)
        : null;

      if (!string.IsNullOrEmpty(variableForParameters) && deliveryMethod == null)
      {
        var message = string.Format("Не найден Способ доставки \"{1}\". Исходящее письмо с Id: \"{0}\". ", documentId, variableForParameters);
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
        logger.Warn(message);
      }

      try
      {
        logger.Debug("Отправка в ОДата.");
        var addressee = outgoingLetter.CreateAddressee(contact, counterparty, deliveryMethod, logger);
        logger.Debug("новую строку адресата добавлена.");
      }
      catch (Exception ex)
      {
        exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = ex.Message });
        logger.Error(ex, ex.Message);
        return exceptionList;
      }

      return exceptionList;
    }
  }
}
