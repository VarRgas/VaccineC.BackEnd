namespace VaccineC.Query.Model.Models
{
    public class UserResource
    {
        public Guid ID { get; set; }
        public Guid UsersId { get; set; }
        public Guid ResourcesId { get; set; }
        public DateTime Register { get; set; }
    }
}
