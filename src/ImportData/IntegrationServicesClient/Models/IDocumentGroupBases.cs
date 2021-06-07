namespace ImportData.IntegrationServicesClient.Models
{
    public class IDocumentGroupBases
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
