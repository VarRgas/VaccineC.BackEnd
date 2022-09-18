using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationSummarySituationViewModel
    {
        public string ProductName { get; set; }
        public int TotalAuthorizationsMonth { get; set; }
        public int TotalUnitsProduct { get; set; }
        public int TotalUnitsAfterApplication { get; set; }

    }
}
