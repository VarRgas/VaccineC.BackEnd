using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Query.Model.Models
{
    public class CompanyParameter
    {
        public Guid ID { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? DefaultPaymentFormId { get; set; }
        public int ApplicationTimePerMinute { get; set; }
        public int MaximumDaysBudgetValidity { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinalTime { get; set; }
        public DateTime Register { get; set; }
        public Company? Company { get; set; }

        [ForeignKey("DefaultPaymentFormId")]
        public PaymentForm? PaymentForm { get; set; }   
       
    }
}
