namespace VaccineC.Query.Model.Models
{
    public class Application
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public Guid? BudgetId { get; set; }
        public DateTime InclusionDate { get; set; }
        public string Situation { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string DoseType { get; set; }
        public string RouteOfAdministration { get; set; }
        public string ApplicationPlace { get; set; }
        public string? Details { get; set; }
        public DateTime? Register { get; set; }
        public Guid? MovementProductId { get; set; }
        public Guid AuthorizationId { get; set; }
    }
}
