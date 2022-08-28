using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class ResetPasswordUserCommand : IRequest<UserViewModel>
    {
        public Guid ID;
        public string Password;

        public ResetPasswordUserCommand(Guid id, string password)
        {
            ID = id;
            Password = password;
        }
    }
}
