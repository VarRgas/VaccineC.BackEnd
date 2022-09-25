using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class BudgetNegotiation
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid PaymentFormId { get; set; }
        public decimal TotalAmountBalance { get; set; }
        public decimal TotalAmountTraded { get; set; }
        public int Installments { get; set; }
        public DateTime Register { get; set; }
        public PaymentForm? PaymentForm { get; set; }
    }
}
