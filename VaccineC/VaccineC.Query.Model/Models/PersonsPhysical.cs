namespace VaccineC.Query.Model.Models
{
    public class PersonsPhysical
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime? Register { get; set; }
        public string CnsNumber { get; set; }
        public string CpfNumber { get; set; }
    }
}
