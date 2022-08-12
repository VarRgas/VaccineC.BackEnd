namespace VaccineC.Command.Domain.Entities
{
    public class Person
    {
        public Guid ID { get; set; }
        public string PersonType { get; set; }
        public string Name { get; set; }
        public DateTime CommemorativeDate { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public DateTime Register { get; set; }


    }
}
