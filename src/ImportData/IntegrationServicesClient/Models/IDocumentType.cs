namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Типы документов")]
    public class IDocumentType : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string DocumentTypeGuid { get; set; }
        public string DocumentFlow { get; set; }
        public string Status { get; set; }
        public bool IsRegistrationAllowed { get; set; }
    }
}
