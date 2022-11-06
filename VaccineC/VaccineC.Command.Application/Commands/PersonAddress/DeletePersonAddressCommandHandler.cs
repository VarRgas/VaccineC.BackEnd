using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Command.Application.Commands.PersonAddress
{
    public class DeletePersonAddressCommandHandler : IRequestHandler<DeletePersonAddressCommand, IEnumerable<PersonAddressViewModel>>
    {
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IPersonAddressAppService _personAddressAppService;

        public DeletePersonAddressCommandHandler(IPersonAddressRepository personAddressRepository, IPersonAddressAppService personAddressAppService)
        {
            _personAddressRepository = personAddressRepository;
            _personAddressAppService = personAddressAppService;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> Handle(DeletePersonAddressCommand request, CancellationToken cancellationToken)
        {

            var personAddress = _personAddressRepository.GetById(request.Id);

            if (personAddress == null)
            {
                throw new ArgumentException("Endereço não encontrado!");
            }

            _personAddressRepository.Remove(personAddress);

            await _personAddressRepository.SaveChangesAsync();

            return await _personAddressAppService.GetAllPersonsAddressesByPersonId(personAddress.PersonID);
        }
    }
}
