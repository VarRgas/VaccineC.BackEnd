namespace VaccineC.Query.Application.ViewModels
{
    public class PersonsPhysicalViewModel
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime Register { get; set; }
        public string? CnsNumber { get; set; }
        public string? CpfNumber { get; set; }
        public PersonViewModel? Person { get; set; }
    }
}