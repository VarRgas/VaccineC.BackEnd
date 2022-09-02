namespace VaccineC.Query.Model.Models
{
    public class PersonsJuridical
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string? FantasyName { get; set; }
        public string? CnpjNumber { get; set; }
        public DateTime Register { get; set; }
    }
}
