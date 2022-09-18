using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ProductBelowMinimumViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int MinimumStock { get; set; }
        public int TotalUnits { get; set; }
    }
}
