namespace VaccineC.Query.Application.ViewModels
{
    public class ExampleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public bool HasPending { get; set; }
        public decimal AmountPending { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
