namespace ImportData.IntegrationServicesClient.Models
{
    [EntityName("Журналы регистрации")]
    public class IDocumentRegisters : IEntity
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string Index { get; set; }
        public int NumberOfDigitsInNumber { get; set; }
        public string ValueExample { get; set; }
        public string DisplayName { get; set; }
        public string DocumentFlow { get; set; }
        public string RegisterType { get; set; }
        public string NumberingPeriod { get; set; }
        public string NumberingSection { get; set; }
        public string Status { get; set; }
        public IRegistrationGroups RegistrationGroup { get; set; }
    }
}
