using System;

namespace ImportData.IntegrationServicesClient.Models
{
  [EntityName("Официальный документ")]
  public class IOfficialDocuments : IElectronicDocuments
  {
    public string RegistrationNumber { get; set; }
    public DateTimeOffset? RegistrationDate { get; set; }
    public string Subject { get; set; }
    public string Note { get; set; }
    public DateTimeOffset DocumentDate { get; set; }
    public string LifeCycleState { get; set; }
    public string RegistrationState { get; set; }
    public IDocumentRegisters DocumentRegister { get; set; }
    public IDocumentKinds DocumentKind { get; set; }
    public IOfficialDocuments LeadingDocument { get; set; }
    public IDocumentGroupBases DocumentGroup { get; set; }
    public IDepartments Department { get; set; }
    public IEmployees DeliveredTo { get; set; }
    public IEmployees OurSignatory { get; set; }
    public IEmployees PreparedBy { get; set; }
  }
  public static class IOfficialDocumentsExtensions
  {
    /// <summary>
    /// Обновить свойство LifeCycleState.
    /// </summary>
    /// <param name="entity">Сущность, свойство которого необходимо обновить.</param>
    /// <param name="lifeCycleState">Новое значение свойства LifeCycleState.</param>
    /// <returns>Обновленная сущность.</returns>
    public static T UpdateLifeCycleState<T>(this T entity, string lifeCycleState) where T : IOfficialDocuments
    {
      var data = Client.Instance()
                       .For<T>()
                       .Key(entity)
                       .Set(new { LifeCycleState = lifeCycleState })
                       .UpdateEntryAsync().Result;

      return data;
    }
  }
}
