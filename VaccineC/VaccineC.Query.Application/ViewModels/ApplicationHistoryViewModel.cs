using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ApplicationHistoryViewModel
    {
        public Guid ApplicationId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime InclusionDate { get; set; }
        public string ApplicationPlace { get; set; }
        public string DoseType { get; set; }
        public string RouteOfAdministration { get; set; }
        public DateTime? Register { get; set; }
        public string ProductName { get; set; }
        public string Batch { get; set; }
        public Guid ProductSummaryBatchId { get; set; }
        public string UserPersonName { get; set; }
        public int BudgetNumber { get; set; }
        public string PersonResponsible { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public string? SipniIntegrationId { get; set; }
        public Guid? SbimVaccineId { get; set; }

        public string? IntegrationSituation { get; set; }
    }
}
