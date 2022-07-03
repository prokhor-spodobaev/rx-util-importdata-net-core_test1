namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Пользователи")]
    public class IUsers : IRecipients
  {
        public ILogins Login { get; set; }
        public IDepartments Department { get; set; }
        public IPersons Person { get; set; }
        public IJobTitles JobTitle { get; set; }
    }
}
