using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportData.IntegrationServicesClient.Models
{
    public class ICertificates
    {
        //Сведения о сертификате
        public string Description { get; set; }

        //Признак действующего сертификата.Свойство возвращает true, если сертификат действующий, иначе false
        public Nullable<bool> Enabled { get; set; }

        //Издатель сертификата (кем выдан)
        public string Issuer { get; set; }

        //Дата завершения срока действия сертификата
        public Nullable<DateTime> NotAfter { get; set; }

        //Дата начала срока действия сертификата
        public Nullable<DateTime> NotBefore { get; set; }

        //Владелец сертификата (кому выдан)
        public IUsers Owner { get; set; }

        //Сведения о владельце сертификата.Например, наименование организации, должность, ИНН
        public string Subject { get; set; }

        //Отпечаток сертификата
        string Thumbprint { get; set; }

        //Цифровой сертификат X.509
        public byte[] X509Certificate { get; set; }




    }
}
