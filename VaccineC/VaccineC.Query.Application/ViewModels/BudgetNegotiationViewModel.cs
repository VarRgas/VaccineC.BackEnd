using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class BudgetNegotiationViewModel
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid PaymentFormId { get; set; }
        public decimal TotalAmountBalance { get; set; }
        public decimal TotalAmountTraded { get; set; }
        public int Installments { get; set; }
        public DateTime Register { get; set; }
        public PaymentFormViewModel? PaymentForm { get; set; }
        public Guid? UserId { get; set; }
    }
}
