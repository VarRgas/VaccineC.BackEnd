using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class BudgetProduct
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? BorrowerPersonId { get; set; }
        public string? ProductDose { get; set; }
        public string? Details { get; set; }
        public decimal EstimatedSalesValue { get; set; }
        public string SituationProduct { get; set; }
        public DateTime Register { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("BorrowerPersonId")]
        public Person? Person { get; set; }
    }
}
