using System;
using System.Collections.Generic;
using System.Text;

namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Срок хранения дела")]
    public class IFileRetentionPeriods : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public int? RetentionPeriod { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
