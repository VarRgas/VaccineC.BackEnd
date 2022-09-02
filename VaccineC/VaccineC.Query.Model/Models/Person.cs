namespace VaccineC.Query.Model.Models
{
    public class Person
    {
        public Guid ID { get; set; }
        public string PersonType { get; set; }
        public string Name { get; set; }
        public DateTime? CommemorativeDate { get; set; }
        public string? Email { get; set; }
        public string? ProfilePic { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
        public PersonsJuridical? PersonsJuridical { get; set; }
        public PersonsPhysical? PersonsPhysical { get; set; }

    }
}
