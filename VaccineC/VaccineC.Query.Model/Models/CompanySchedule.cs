namespace VaccineC.Query.Model.Models
{
    public class CompanySchedule
    {
        public Guid ID { get; set; }
        public Guid CompanyId { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinalTime { get; set; }
        public DateTime Register { get; set; }
    }
}
