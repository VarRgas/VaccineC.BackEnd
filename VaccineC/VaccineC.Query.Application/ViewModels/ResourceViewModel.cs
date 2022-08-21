using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ResourceViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public DateTime Register { get; set; }
    }
}
