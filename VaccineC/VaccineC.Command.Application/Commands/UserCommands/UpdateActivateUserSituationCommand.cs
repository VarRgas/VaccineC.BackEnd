using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateActivateUserSituationCommand : IRequest<UserViewModel>
    {
        public Guid ID;

        public UpdateActivateUserSituationCommand(Guid id)
        {
            ID = id;
        }

    }
}