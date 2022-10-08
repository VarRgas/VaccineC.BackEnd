using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationParameterViewModel
    {
        public int ApplicationTimePerMinute { get; set; }
        public string ScheduleColor { get; set; }
        public TimeSpan MaxTime { get; set; }
        public TimeSpan MinTime { get; set; }
    }
}
