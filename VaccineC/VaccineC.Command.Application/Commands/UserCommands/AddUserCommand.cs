using MediatR;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<Guid>
    {
        public Guid ID;
        public Guid PersonID;
        public string Email;
        public string Password;
        public string Situation;
        public DateTime Register;

        public AddUserCommand(Guid id, Guid personId, string email, string password, string situation, DateTime register)
        {
            ID = id;
            PersonID = personId;
            Email = email;
            Password = password;
            Situation = situation;
            Register = register;
        }
    }
}
