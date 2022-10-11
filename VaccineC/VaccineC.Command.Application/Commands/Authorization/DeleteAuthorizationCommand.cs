using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class DeleteAuthorizationCommand : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public Guid Id;
        public Guid userId;

        public DeleteAuthorizationCommand(Guid id, Guid userId)
        {
            Id = id;
            this.userId = userId;   
        }
    }
}
