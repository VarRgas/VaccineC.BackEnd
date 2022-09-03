using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonAddress
{
    public class AddPersonAddressComandHandler : IRequestHandler<AddPersonAddressComand, IEnumerable<PersonAddressViewModel>>
    {
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IPersonAddressAppService _personAddressAppService;

        public AddPersonAddressComandHandler(IPersonAddressRepository personAddressRepository, IPersonAddressAppService personAddressAppService)
        {
            _personAddressRepository = personAddressRepository;
            _personAddressAppService = personAddressAppService;
        }
        public async Task<IEnumerable<PersonAddressViewModel>> Handle(AddPersonAddressComand request, CancellationToken cancellationToken)
        {

            Domain.Entities.PersonAddress newPersonAddress = new Domain.Entities.PersonAddress(
                Guid.NewGuid(),
                request.PersonID,
                request.AddressType,
                request.PublicPlace,
                request.District,
                request.AddressNumber,
                request.Complement,
                request.AddressCode,
                request.ReferencePoint,
                request.City,
                request.State,
                request.Country,
                DateTime.Now
            );

            _personAddressRepository.Add(newPersonAddress);
            await _personAddressRepository.SaveChangesAsync();

            return await _personAddressAppService.GetAllPersonsAddressesByPersonId(newPersonAddress.PersonID);
        }
    }
}
