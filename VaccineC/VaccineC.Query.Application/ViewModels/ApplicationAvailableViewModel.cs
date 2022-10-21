using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ApplicationAvailableViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string DoseType { get; set; }
        public Guid AuthorizationId { get; set; }
        public int AuthorizationNumber { get; set; }
        public string TypeOfService { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public Guid BudgetId { get; set; }
        public int BudgetNumber { get; set; }
        public string PersonBudget { get; set; }
        public decimal TotalBudgetAmount { get; set; }

    }
}
