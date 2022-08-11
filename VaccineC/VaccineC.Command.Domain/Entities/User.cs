namespace VaccineC.Command.Domain.Entities
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Person Person { get; set; }
    }
}
