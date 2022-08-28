using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateDeactivateUserSituationCommand : IRequest<UserViewModel>
    {
        public Guid ID;

        public UpdateDeactivateUserSituationCommand(Guid id)
        {
            ID = id;
        }

    }
}
