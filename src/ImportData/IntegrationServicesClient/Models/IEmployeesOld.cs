namespace ImportData.IntegrationServicesClient.Models.OldModels
{
    /// <summary>
    /// Модель сущности Сотрудник для вресии DirectumRX 4.3 и ниже.
    /// </summary>
    [EntityName("Сотрудник")]
    public class IEmployees : IUsers
    {
        public string Phone { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public bool? NeedNotifyExpiredAssignments { get; set; }
        public bool? NeedNotifyNewAssignments { get; set; }
        public string PersonnelNumber { get; set; }
    }
}
