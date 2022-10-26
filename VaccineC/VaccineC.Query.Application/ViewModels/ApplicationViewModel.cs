namespace VaccineC.Query.Application.ViewModels
{
    public class ApplicationViewModel
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public Guid? BudgetProductId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string DoseType { get; set; }
        public string RouteOfAdministration { get; set; }
        public string ApplicationPlace { get; set; }
        public DateTime? Register { get; set; }
        public Guid? ProductSummaryBatchId { get; set; }
        public Guid AuthorizationId { get; set; }
        public AuthorizationViewModel? Authorization { get; set; }
        public string? SipniIntegrationId { get; set; }
    }
}
