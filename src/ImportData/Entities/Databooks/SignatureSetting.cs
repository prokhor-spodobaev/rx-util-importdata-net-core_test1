using System.Collections.Generic;
using System.Globalization;
using NLog;
using System.Linq;
using ImportData.IntegrationServicesClient.Models;
using System;

namespace ImportData.Entities.Databooks
{
    internal class SignatureSetting : Entity
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
        public override IEnumerable<Structures.ExceptionsStruct> SaveToRX(Logger logger, bool supplementEntity, string ignoreDuplicates, int shift = 0, bool isBatch = false)
        {
            var exceptionList = new List<Structures.ExceptionsStruct>();

            var signatory = this.Parameters[shift + 0].Trim();

            var signatoryObj = BusinessLogic.GetEntityWithFilter<IRecipients>(x => x.Name == signatory, exceptionList, logger);
            if (signatoryObj == null)
            {
                var message = string.Format("Не удалось найти соответствующего сотрудника \"{0}\".", signatory);
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                logger.Error(message);

                return exceptionList;
            }

            var reason = this.Parameters[shift + 1].Trim();
            var reasonDict = new Dictionary<string, string>()
            {
                { "Должностные обязанности", "Duties" },
                { "Доверенность", "PowerOfAttorney" },
                { "Электронная доверенность", "FormalizedPoA" },
                { "Другой документ", "Other" },
            };

            if (!reasonDict.ContainsValue(reason))
            {
                reason = reasonDict.GetValueOrDefault(reason);
                if (reason == null)
                {
                    var message = "Неверно указан тип основания";
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            var validTill = this.Parameters[shift + 2].Trim();
            DateTime validTillDate;
            DateTime? validTillDateNullable = null;
            if (DateTime.TryParse(validTill, out validTillDate))
            {
                validTillDateNullable = validTillDate;
            }

            var documentFlow = this.Parameters[shift + 3].Trim();
            var documentFlowDict = new Dictionary<string, string>()
            {
                { "Любой", "All" },
                { "Входящий", "Incoming" },
                { "Исходящий", "Outgoing" },
                { "Внутренний", "Inner" },
                { "Договоры", "Contracts" },
            };

            if (!documentFlowDict.ContainsValue(documentFlow))
            {
                documentFlow = documentFlowDict.GetValueOrDefault(documentFlow);
                if (documentFlow == null)
                {
                    var message = "Неверно указан документопоток";
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            var priority = this.Parameters[shift + 4].Trim();


            var validFrom = this.Parameters[shift + 5].Trim();
            DateTime validFromDate;
            DateTime? validFromDateNullable = null;
            if (DateTime.TryParse(validFrom, out validFromDate))
            {
                validFromDateNullable = validFromDate;
            }

            var limit = this.Parameters[shift + 6].Trim();
            var limitDict = new Dictionary<string, string>()
            {
                { "Без ограничений", "NoLimit" },
                { "Общая сумма документа", "Amount" },
            };
            
            if (!limitDict.ContainsValue(limit))
            {
                limit = limitDict.GetValueOrDefault(limit);
                if (limit == null)
                {
                    var message = "Неверно указан лимит";
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            var amount = this.Parameters[shift + 7].Trim();
            int amountInt;
            int? amountIntNullable = null;
            if (int.TryParse(amount, out amountInt))
            {
                amountIntNullable = amountInt;
            }

            var currency = this.Parameters[shift + 8].Trim();
            ICurrencies currencyObj = null;
            if (!string.IsNullOrEmpty(currency))
            {
                currencyObj = BusinessLogic.GetEntityWithFilter<ICurrencies>(x => x.Name == currency, exceptionList, logger);
                if (currencyObj == null)
                {
                    var message = string.Format("Не удалось найти соответствующую валюту\"{0}\".", currency);
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            var note = this.Parameters[shift + 9].Trim();
            var status = this.Parameters[shift + 10].Trim();
            var statusDict = new Dictionary<string, string>()
            {
                { "Действующая", "Active" },
                { "Закрытая", "Closed" },
            };

            if (!statusDict.ContainsValue(status))
            {
                status = statusDict.GetValueOrDefault(status);
                if (status == null)
                {
                    var message = "Неверно указано состояние";
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            var name = this.Parameters[shift + 11].Trim();

            // Временно не используется
            var certificate = this.Parameters[shift + 12].Trim();

            var signingReason = this.Parameters[shift + 13].Trim();

            var jobTitle = this.Parameters[shift + 14].Trim();
            IJobTitles jobTitleObj = null;
            if (!string.IsNullOrEmpty(jobTitle))
            {
                jobTitleObj = BusinessLogic.GetEntityWithFilter<IJobTitles>(x => x.Name == jobTitle, exceptionList, logger);
                if (jobTitleObj == null)
                {
                    var message = string.Format("Не удалось найти соответствующую должность\"{0}\".", jobTitle);
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Error, Message = message });
                    logger.Error(message);
                }
            }

            try
            {
                if (BusinessLogic.GetEntityWithFilter<ISignatureSettings>(x => 
                    x.Recipient == signatoryObj && 
                    x.Note == note && 
                    x.DocumentFlow == documentFlow &&
                    x.Reason == reason &&
                    x.Status == status &&
                    x.Name == name &&
                    x.SigningReason == signingReason
                    , exceptionList, logger) != null)
                {
                    var message = string.Format("Такая запись уже есть.");
                    exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
                    logger.Warn(message);

                    return exceptionList;
                }

                var signatureSetting = new ISignatureSettings();
                signatureSetting.Currency = currencyObj;
                signatureSetting.Status = status; //
                //signatureSetting.BusinessUnits = null;
                signatureSetting.SigningReason = signingReason; //
                signatureSetting.DocumentFlow = documentFlow; //
                signatureSetting.Reason = reason; //
                signatureSetting.Amount = amountIntNullable; //
                signatureSetting.Limit = limit; //
                signatureSetting.Recipient = signatoryObj;
                signatureSetting.Certificate = null;
                //signatureSetting.Departments = null;
                signatureSetting.Document = null;
                //signatureSetting.Categories = null;
                signatureSetting.DocumentInfo = null; //
                //signatureSetting.DocumentKinds = null;
                signatureSetting.IsSystem = false; //
                signatureSetting.IsSystemCustomSC = false; //
                signatureSetting.JobTitle = jobTitleObj;
                signatureSetting.Name = name; //
                signatureSetting.Note = note; //
                signatureSetting.Priority = int.Parse(priority); //
                signatureSetting.ValidFrom = validFromDateNullable; //
                signatureSetting.ValidTill = validTillDateNullable; //

                var createdSignatureSetting = BusinessLogic.CreateEntity<ISignatureSettings>(signatureSetting, exceptionList, logger);


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
