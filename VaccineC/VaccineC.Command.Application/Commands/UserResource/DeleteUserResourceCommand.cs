using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class DeleteUserResourceCommand : IRequest<IEnumerable<ResourceViewModel>>
    {
        public Guid Id;

        public DeleteUserResourceCommand(Guid id)
        {
            Id = id;
        }
    }
}