namespace VaccineC.Query.Application.ViewModels
{
    public class LoginViewModel
    {
        public Guid ID { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }
        public string PersonName { get; set; }
        public Guid PersonID { get; set; }
        public string? PersonProfilePic { get; set; }

    }
}
