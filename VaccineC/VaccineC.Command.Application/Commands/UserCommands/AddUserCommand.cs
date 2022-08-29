using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<UserViewModel>
    {
        public Guid ID;
        public Guid PersonID;
        public string Email;
        public string Password;
        public string Situation;
        public string FunctionUser;
        public DateTime Register;

        public AddUserCommand(Guid id, Guid personId, string email, string password, string situation, string functionUser, DateTime register)
        {
            ID = id;
            PersonID = personId;
            Email = email;
            Password = password;
            Situation = situation;
            FunctionUser = functionUser;
            Register = register;
        }
    }
}
