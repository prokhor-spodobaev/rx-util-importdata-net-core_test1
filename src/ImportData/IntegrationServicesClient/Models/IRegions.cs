namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Регионы")]
    public class IRegions : IEntity
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
