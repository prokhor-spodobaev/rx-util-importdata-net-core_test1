namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Способы доставки")]
    public class IMailDeliveryMethods : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Note { get; set; }
        public string Sid { get; set; }
        public string Status { get; set; }
    }
}
