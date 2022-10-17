using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public Guid ID { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("budgetProductId")]
        public Guid? BudgetProductId { get; set; }

        [Column("inclusionDate", TypeName = "datetime")]
        public DateTime InclusionDate { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("applicationDate", TypeName = "datetime")]
        public DateTime? ApplicationDate { get; set; }

        [Column("doseType", TypeName = "varchar(2)")]
        public string DoseType { get; set; }

        [Column("routeOfAdministration", TypeName = "varchar(1)")]
        public string RouteOfAdministration { get; set; }

        [Column("applicationPlace", TypeName = "varchar(2)")]
        public string ApplicationPlace { get; set; }

        [Column("details", TypeName = "text")]
        public string? Details { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime? Register { get; set; }

        [Column("productSummaryBatchId")]
        public Guid? ProductSummaryBatchId { get; set; }

        [Column("authorizationId")]
        public Guid AuthorizationId { get; set; }

        public Application(Guid id,
                           Guid userId,
                           Guid? budgetProductId,
                           DateTime inclusionDate,
                           string situation,
                           DateTime? applicationDate,
                           string doseType,
                           string routeOfAdministration,
                           string applicationPlace,
                           string? details,
                           DateTime register,
                           Guid? productSummaryBatchId,
                           Guid authorizationId
            )
        {
            ID = id;
            UserId = userId;
            BudgetProductId = budgetProductId;
            InclusionDate = inclusionDate;
            Situation = situation;
            ApplicationDate = applicationDate;
            DoseType = doseType;
            RouteOfAdministration = routeOfAdministration;
            ApplicationPlace = applicationPlace;
            Details = details;
            Register = register;
            ProductSummaryBatchId = productSummaryBatchId;
            AuthorizationId = authorizationId;
        }

        public Application()
        {

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetBudgetProductId(Guid? budgetProductId)
        {
            BudgetProductId = budgetProductId;
        }

        public void SetInclusionDate(DateTime inclusionDate)
        {
            InclusionDate = inclusionDate;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetApplicationDate(DateTime? applicationDate)
        {
            ApplicationDate = applicationDate;
        }

        public void SetDoseType(string doseType)
        {
            DoseType = doseType;
        }

        public void SetRouteOfAdministration(string routeOfAdministration)
        {
            RouteOfAdministration = routeOfAdministration;
        }

        public void SetApplicationPlace(string applicationPlace)
        {
            ApplicationPlace = applicationPlace;
        }

        public void SetDetails(string details)
        {
            Details = details;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetProductSummaryBatchId(Guid? productSummaryBatchId)
        {
            ProductSummaryBatchId = productSummaryBatchId;
        }

        public void SetAuthorizationId(Guid authorizationId)
        {
            AuthorizationId = authorizationId;
        }
    }
}
