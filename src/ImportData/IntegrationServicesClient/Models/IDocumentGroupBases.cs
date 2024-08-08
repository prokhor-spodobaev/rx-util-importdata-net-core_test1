namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Группа документов")]
    public class IDocumentGroupBases : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
