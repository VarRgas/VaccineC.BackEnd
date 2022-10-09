using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class Event
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public string Situation { get; set; }
        public string Concluded { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
    }
}
