using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationDashInfoViewModel
    {
        public int AuthorizationScheduleNumber { get; set; }
        public int AuthorizationCanceledNumber { get; set; }
        public int AuthorizationConcludedNumber { get; set; }
        public int AuthorizationsWithNotification { get; set; }
        public int AuthorizationsWithoutNotification { get; set; }

        public List<AuthorizationNotificationDashInfo> authorizationNotificationDashInfos = new List<AuthorizationNotificationDashInfo>();
    }
}
