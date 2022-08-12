using MediatR;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Situation { get; set; }
        public DateTime Register { get; set; }
        public string PersonType { get; set; }
    }
}
