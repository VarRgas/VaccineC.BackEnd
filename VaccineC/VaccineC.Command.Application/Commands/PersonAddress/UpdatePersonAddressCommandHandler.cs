using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonAddress
{
    public class UpdatePersonAddressCommandHandler : IRequestHandler<UpdatePersonAddressCommand, IEnumerable<PersonAddressViewModel>>
    {
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IPersonAddressAppService _personAddressAppService;

        public UpdatePersonAddressCommandHandler(IPersonAddressRepository personAddressRepository, IPersonAddressAppService personAddressAppService)
        {
            _personAddressRepository = personAddressRepository;
            _personAddressAppService = personAddressAppService;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> Handle(UpdatePersonAddressCommand request, CancellationToken cancellationToken)
        {
            var personAddress = _personAddressRepository.GetById(request.ID);
            personAddress.SetAddressType(request.AddressType);
            personAddress.SetPublicPlace(request.PublicPlace);
            personAddress.SetDistrict(request.District);
            personAddress.SetAddressNumber(request.AddressNumber);
            personAddress.SetComplement(request.Complement);
            personAddress.SetAddressCode(request.AddressCode);
            personAddress.SetReferencePoint(request.ReferencePoint);
            personAddress.SetCity(request.City);
            personAddress.SetState(request.State);
            personAddress.SetCountry(request.Country);
            personAddress.SetRegister(DateTime.Now);

            await _personAddressRepository.SaveChangesAsync();

            return await _personAddressAppService.GetAllPersonsAddressesByPersonId(personAddress.PersonID);
        }
    }
}
