using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.ViewModels
{
    public class CompaniesParametersViewModel
    {
        public Guid ID { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? DefaultPaymentFormId { get; set; }
        public int ApplicationTimePerMinute { get; set; }
        public int MaximumDaysBudgetValidity { get; set; }
        public DateTime Register { get; set; }
        public string ScheduleColor { get; set; }
        public CompanyViewModel? Company { get; set; }
        public PaymentFormViewModel? PaymentForm { get; set; }
    }
}
