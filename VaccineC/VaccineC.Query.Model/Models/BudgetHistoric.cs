using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class BudgetHistoric
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid UserId { get; set; }
        public string Historic { get; set; }
        public DateTime Register { get; set; }
        public User? User { get; set; }
    }
}
