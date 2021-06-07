namespace ImportData.IntegrationServicesClient.Models
{
    class IInternalDocumentBases : IOfficialDocuments
    {
        public IEmployees Assignee { get; set; }
        public IBusinessUnits BusinessUnit { get; set; }
    }
}
