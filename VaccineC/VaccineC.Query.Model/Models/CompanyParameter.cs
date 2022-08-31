namespace VaccineC.Query.Model.Models
{
    public class CompanyParameter
    {
        public Guid ID { get; set; }
        public Guid CompanyId { get; set; }
        public int ApplicationTimePerMinute { get; set; }
        public int MaximumDaysBudgetValidity { get; set; }
        public DateTime Register { get; set; }
        public string ScheduleColor { get; set; }
        public Company? Company { get; set; }
    }
}
