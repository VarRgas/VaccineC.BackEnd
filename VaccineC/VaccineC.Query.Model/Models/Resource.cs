using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class Resource
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public DateTime Register { get; set; }
    }
}
