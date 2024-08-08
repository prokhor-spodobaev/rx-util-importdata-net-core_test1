using System;
using System.Collections.Generic;
using System.Text;

namespace ImportData.IntegrationServicesClient.Models
{
	[EntityName("Страны")]
	public class ICountries : IEntity
	{
		public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
		public string Status { get; set; }
		public string Code { get; set; }
	}
}
