using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class NotificationViewModel
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string Situation { get; set; }
        public DateTime Register { get; set; }
    }
}
