namespace VaccineC.Query.Application.ViewModels
{
    public class PersonsJuridicalViewModel
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        public string FantasyName { get; set; }
        public string CnpjNumber { get; set; }
        public DateTime Register { get; set; }
        public PersonViewModel? Person { get; set; }
    }
}
