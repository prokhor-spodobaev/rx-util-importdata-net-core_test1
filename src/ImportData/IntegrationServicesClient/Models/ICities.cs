namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Город")]
    public class ICities : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Status { get; set; }
    }
}
