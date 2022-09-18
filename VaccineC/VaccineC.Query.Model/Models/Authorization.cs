using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class Authorization
    {
        public Guid ID { get; set; }
        public int AuthorizationNumber { get; set; }
        public Guid UserId { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public DateTime Register { get; set; }
        public Guid BorrowerPersonId { get; set; }
        public Guid ProviderPersonId { get; set; }
        public string Situation { get; set; }
        public string TypeOfService { get; set; }
        public string Notify { get; set; }
        public Guid EventId { get; set; }
        public Guid BudgetProductId { get; set; }
        public BudgetProduct? BudgetProduct { get; set; }
    }
}
