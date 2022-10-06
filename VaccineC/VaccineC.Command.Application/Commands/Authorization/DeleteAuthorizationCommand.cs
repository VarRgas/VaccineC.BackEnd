using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class DeleteAuthorizationCommand : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public Guid Id;
        public DeleteAuthorizationCommand(Guid id)
        {
            Id = id;
        }
    }
}
