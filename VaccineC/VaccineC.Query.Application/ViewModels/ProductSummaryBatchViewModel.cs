using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class ProductSummaryBatchViewModel
    {
        public Guid ID { get; set; }
        public string Batch { get; set; }
        public decimal NumberOfUnitsBatch { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ValidityBatchDate { get; set; }
        public DateTime Register { get; set; }
        public string Manufacturer { get; set; }
        public Guid ProductsId { get; set; }
    }
}
