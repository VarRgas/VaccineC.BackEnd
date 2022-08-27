using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Situation { get; set; }
        public string FunctionUser { get; set; }
        public DateTime Register { get; set; }
        public Guid PersonId { get; set; }
    }
}
