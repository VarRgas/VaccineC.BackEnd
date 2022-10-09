using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class UpdateAuthorizationCommand : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public Guid ID;
        public Guid UserId;
        public Guid EventId;
        public Guid BudgetProductId;
        public Guid BorrowerPersonId;
        public int AuthorizationNumber;
        public string Situation;
        public string TypeOfService;
        public string Notify;
        public DateTime AuthorizationDate;
        public DateTime Register;

        public UpdateAuthorizationCommand(Guid id, Guid userId, Guid eventId, Guid budgetProductId, Guid borrowerPersonId, int authorizationNumber, string situation, string typeOfService, string notify, DateTime authorizationDate, DateTime register)
        {
            ID = id;
            UserId = userId;
            EventId = eventId;
            BudgetProductId = budgetProductId;
            BorrowerPersonId = borrowerPersonId;
            AuthorizationNumber = authorizationNumber;
            Situation = situation;
            TypeOfService = typeOfService;
            Notify = notify;
            AuthorizationDate = authorizationDate;
            Register = register;
        }
    }
}
