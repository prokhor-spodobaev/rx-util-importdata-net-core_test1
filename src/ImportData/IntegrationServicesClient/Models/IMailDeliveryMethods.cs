namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Способы доставки")]
    public class IMailDeliveryMethods
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Sid { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
    }
}
