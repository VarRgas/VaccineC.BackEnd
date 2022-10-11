using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationNotificationViewModel
    {
        public Guid ID { get; set; }
        public Guid AuthorizationId { get; set; }
        public Guid EventId { get; set; }
        public string PersonPhone { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public TimeSpan SendHour { get; set; }
        public DateTime Register { get; set; }
    }
}
