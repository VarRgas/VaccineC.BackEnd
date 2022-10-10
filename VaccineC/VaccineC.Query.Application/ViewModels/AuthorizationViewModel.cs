using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.ViewModels
{
    public class AuthorizationViewModel
    {
        public Guid ID { get; set; }
        public int AuthorizationNumber { get; set; }
        public Guid UserId { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public DateTime Register { get; set; }
        public Guid BorrowerPersonId { get; set; }
        public string Situation { get; set; }
        public string TypeOfService { get; set; }
        public string Notify { get; set; }
        public Guid EventId { get; set; }
        public Guid BudgetProductId { get; set; }
        public BudgetProductViewModel? BudgetProduct { get; set; }
        public Event? Event { get; set; }
        public PersonViewModel? Person { get; set; }

    }
}
