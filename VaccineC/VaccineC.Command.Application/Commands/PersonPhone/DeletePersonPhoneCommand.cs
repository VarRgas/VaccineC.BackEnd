using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Command.Application.Commands.PersonPhone
{
    public class DeletePersonPhoneCommand : IRequest<IEnumerable<PersonPhoneViewModel>>
    {
        public Guid Id;
        public DeletePersonPhoneCommand(Guid id)
        {
            Id = id;
        }
    }
}
