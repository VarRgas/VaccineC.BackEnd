using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.ViewModels
{
    public class BudgetHistoricViewModel
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid UserId { get; set; }
        public string Historic { get; set; }
        public DateTime Register { get; set; }
        public UserViewModel? User { get; set; }
    }
}
