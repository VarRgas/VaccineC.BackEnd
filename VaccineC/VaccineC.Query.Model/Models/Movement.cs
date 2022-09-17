using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class Movement
    {
        public Guid ID { get; set; }
        public int MovementNumber { get; set; }
        public Guid UsersId { get; set; }
        public string MovementType { get; set; }
        public decimal? ProductsAmount { get; set; }
        public DateTime Register { get; set; }
        public string Situation { get; set; }
        //public Product? Product { get; set; }
    }

}
