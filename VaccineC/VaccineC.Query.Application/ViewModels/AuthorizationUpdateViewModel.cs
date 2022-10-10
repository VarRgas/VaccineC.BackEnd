using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationUpdateViewModel
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public DateTime Register { get; set; }
        public Guid EventId { get; set; }
        public DateTime StartDateEvent { get; set; }
        public TimeSpan StartTimeEvent { get; set; }
    }
}
