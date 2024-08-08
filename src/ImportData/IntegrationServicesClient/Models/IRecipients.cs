namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Субъект прав")]
    public class IRecipients : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public string Status { get; set; }
    }
}
