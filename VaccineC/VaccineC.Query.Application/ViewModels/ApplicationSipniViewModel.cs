using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ApplicationSipniViewModel
    {
        public Guid ID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Product { get; set; }
        public string Borrower { get; set; }
        public Boolean isComunicated { get; set; }
    }
}
