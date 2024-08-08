namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Виды документов")]
    public class IDocumentKinds : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Note { get; set; }
        public int DeadlineInDays { get; set; }
        public string ShortName { get; set; }
        public int DeadlineInHours { get; set; }
        public bool GenerateDocumentName { get; set; }
        public bool AutoNumbering { get; set; }
        public bool ProjectsAccounting { get; set; }
        public bool GrantRightsToProject { get; set; }
        public bool IsDefault { get; set; }
        public string Code { get; set; }
        public string DocumentFlow { get; set; }
        public string NumberingType { get; set; }
        public string Status { get; set; }
        public IDocumentType DocumentType { get; set; }
    }
}
