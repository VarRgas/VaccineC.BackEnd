namespace VaccineC.Command.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Type { get; set; }
        public DateTime CommemorativeDate { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public DateTime RegisterDate { get; set; }


    }
}
