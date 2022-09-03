using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class PersonAddress
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string AddressType { get; set; }
        public string PublicPlace { get; set; }
        public string District { get; set; }
        public string AddressNumber { get; set; }
        public string Complement { get; set; }
        public string AddressCode { get; set; }
        public string? ReferencePoint { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime Register { get; set; }
    }
}
