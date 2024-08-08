namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Должность")]
    public  class IJobTitles : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Status { get; set; }
    }
}
