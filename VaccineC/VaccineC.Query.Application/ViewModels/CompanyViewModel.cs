namespace VaccineC.Query.Application.ViewModels
{
    public class CompanyViewModel
    {
        public Guid ID { get; set; }
        public Guid PersonId { get; set; }
        public string Details { get; set; }
        public DateTime Register { get; set; }
        public PersonViewModel? Person { get; set; }
    }
}
