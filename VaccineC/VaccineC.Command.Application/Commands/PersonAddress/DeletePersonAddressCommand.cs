using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Command.Application.Commands.PersonAddress
{
    public class DeletePersonAddressCommand : IRequest<IEnumerable<PersonAddressViewModel>>
    {
        public Guid Id;
        public DeletePersonAddressCommand(Guid id)
        {
            Id = id;
        }
    }
}
