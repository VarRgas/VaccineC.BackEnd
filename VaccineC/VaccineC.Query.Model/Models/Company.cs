namespace VaccineC.Query.Model.Models
{
    public class Company
    {
        public Guid ID { get; set; }
        public Guid PersonId { get; set; }
        public string Details { get; set; }
        public DateTime Register { get; set; }

        public Person? Person { get; set; }
    }
}
