using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class DiscardViewModel
    {
        public Guid ID { get; set; }
        public Guid ProductSummaryBatchId { get; set; }
        public Guid UserId { get; set; }
        public string Batch { get; set; }
        public int DiscardedUnits { get; set; }
        public string Reason { get; set; }
        public DateTime Register { get; set; }
        public ProductSummaryBatchViewModel? ProductSummaryBatch { get; set; }
        public UserViewModel? User { get; set; }
    }
}
