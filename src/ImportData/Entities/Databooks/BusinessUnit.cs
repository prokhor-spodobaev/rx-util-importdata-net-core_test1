using System;
using System.Collections.Generic;
using NLog;
using ImportData.IntegrationServicesClient.Models;

namespace ImportData
{
    class BusinessUnit : Entity
    {
        public int PropertiesCount = 19;
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

            var name = this.Parameters[shift + 0].Trim();

            if (string.IsNullOrEmpty(name))
            {
                var message = string.Format("Не заполнено поле \"Наименование\".");
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = "Error", Message = message });
                logger.Error(message);

                return exceptionList;
            }

            var legalName = this.Parameters[shift + 1].Trim();
            variableForParameters = this.Parameters[shift + 2].Trim();
            var headCompany = BusinessLogic.GetEntityWithFilter<IBusinessUnits>(b => b.Name == variableForParameters, exceptionList, logger);

            if (!string.IsNullOrEmpty(this.Parameters[shift + 2].Trim()) && headCompany == null)
            {
                headCompany = BusinessLogic.CreateEntity<IBusinessUnits>(new IBusinessUnits() { Name = variableForParameters, Status = "Active" }, exceptionList, logger);
            }

            variableForParameters = this.Parameters[shift + 3].Trim();
            var ceo = BusinessLogic.GetEntityWithFilter<IEmployees>(e => e.Name == variableForParameters, exceptionList, logger);

            if (!string.IsNullOrEmpty(this.Parameters[shift + 3].Trim()) && ceo == null)
            {
                var message = string.Format("Не найден Руководитель \"{1}\". Наименование НОР: \"{0}\". ", name, this.Parameters[shift + 3].Trim());
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
                logger.Warn(message);
            }

            var tin = this.Parameters[shift + 4].Trim();
            var trrc = this.Parameters[shift + 5].Trim();
            var psrn = this.Parameters[shift + 6].Trim();
            var nceo = this.Parameters[shift + 7].Trim();
            var ncea = this.Parameters[shift + 8].Trim();
            variableForParameters = this.Parameters[shift + 9].Trim();
            var city = BusinessLogic.GetEntityWithFilter<ICities>(c => c.Name == variableForParameters, exceptionList, logger);

            if (!string.IsNullOrEmpty(this.Parameters[shift + 9].Trim()) && city == null)
            {
                var message = string.Format("Не найден Населенный пункт \"{1}\". Наименование НОР: \"{0}\". ", name, this.Parameters[shift + 9].Trim());
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
                logger.Warn(message);
            }

            variableForParameters = this.Parameters[shift + 10].Trim();
            var region = BusinessLogic.GetEntityWithFilter<IRegions>(r => r.Name == variableForParameters, exceptionList, logger);

            if (!string.IsNullOrEmpty(this.Parameters[shift + 10].Trim()) && region == null)
            {
                var message = string.Format("Не найден Регион \"{1}\". Наименование НОР: \"{0}\". ", name, this.Parameters[shift + 10].Trim());
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
                logger.Warn(message);
            }

            var legalAdress = this.Parameters[shift + 11].Trim();
            var postalAdress = this.Parameters[shift + 12].Trim();
            var phones = this.Parameters[shift + 13].Trim();
            var email = this.Parameters[shift + 14].Trim();
            var homepage = this.Parameters[shift + 15].Trim();
            var note = this.Parameters[shift + 16].Trim();
            var account = this.Parameters[shift + 17].Trim();

            variableForParameters = this.Parameters[shift + 18].Trim();
            var bank = BusinessLogic.GetEntityWithFilter<IBanks>(b => b.Name == variableForParameters, exceptionList, logger);

            if (!string.IsNullOrEmpty(this.Parameters[shift + 18]) && bank == null)
            {
                var message = string.Format("Не найден Банк \"{1}\". Наименование НОР: \"{0}\". ", name, this.Parameters[shift + 18].Trim());
                exceptionList.Add(new Structures.ExceptionsStruct { ErrorType = Constants.ErrorTypes.Warn, Message = message });
                logger.Warn(message);
            }

            try
            {
                var busunessUnit = new IBusinessUnits();

                busunessUnit.Name = name;
                busunessUnit.LegalName = legalName;
                busunessUnit.HeadCompany = headCompany;
                busunessUnit.CEO = ceo;
                busunessUnit.TIN = tin;
                busunessUnit.TRRC = trrc;
                busunessUnit.PSRN = psrn;
                busunessUnit.NCEO = nceo;
                busunessUnit.NCEA = ncea;
                busunessUnit.City = city;
                busunessUnit.Region = region;
                busunessUnit.LegalAddress = legalAdress;
                busunessUnit.PostalAddress = postalAdress;
                busunessUnit.Phones = phones;
                busunessUnit.Email = email;
                busunessUnit.Homepage = homepage;
                busunessUnit.Note = note;
                busunessUnit.Account = account;
                busunessUnit.Bank = bank;
                busunessUnit.Status = "Active";

                BusinessLogic.CreateEntity<IBusinessUnits>(busunessUnit, exceptionList, logger);
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
