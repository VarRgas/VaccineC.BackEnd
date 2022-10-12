using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationSuggestionViewModel
    {
        public Guid BudgetProductId { get; set; }
        public Guid BudgetId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BorrowerId { get; set; }
        public string DoseType { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
    }
}
