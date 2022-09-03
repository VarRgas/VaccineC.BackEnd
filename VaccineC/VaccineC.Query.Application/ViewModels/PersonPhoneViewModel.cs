using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class PersonPhoneViewModel
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string PhoneType { get; set; }
        public string NumberPhone { get; set; }
        public string CodeArea { get; set; }
        public DateTime Register { get; set; }
    }
}
