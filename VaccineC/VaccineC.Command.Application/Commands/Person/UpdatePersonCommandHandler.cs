using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Person
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonViewModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonPhysicalRepository _personPhysicalRepository;
        private readonly IPersonJuridicalRepository _personJuridicalRepository;

        private readonly VaccineCContext _context;

        public UpdatePersonCommandHandler(IPersonRepository personRepository, IPersonPhysicalRepository personPhysicalRepository, IPersonJuridicalRepository personJuridicalRepository, VaccineCContext context)
        {
            _personRepository = personRepository;
            _personPhysicalRepository = personPhysicalRepository;
            _personJuridicalRepository = personJuridicalRepository;
            _context = context;
        }

        public async Task<PersonViewModel> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {

            var person = _personRepository.GetById(request.ID);

            if (!person.PersonType.Equals(request.PersonType))
            {
                if (person.PersonType.Equals("F"))
                {
                    var personPhysical = _context.PersonsPhysical
                        .Where(pp => pp.PersonID == person.ID)
                        .FirstOrDefault();

                    if (personPhysical != null)
                    {
                        var personPhysicalRemove = _personPhysicalRepository.GetById(personPhysical.ID);
                        _personPhysicalRepository.Remove(personPhysicalRemove);
                        await _personPhysicalRepository.SaveChangesAsync();
                    }
                }
                else
                {
                    var personJuridical = _context.PersonsJuridical
                        .Where(pp => pp.PersonID == person.ID)
                        .FirstOrDefault();

                    if (personJuridical != null)
                    {
                        var personJuridicalRemove = _personJuridicalRepository.GetById(personJuridical.ID);
                        _personJuridicalRepository.Remove(personJuridicalRemove);
                        await _personJuridicalRepository.SaveChangesAsync();
                    }
                }

            }

            person.SetPersonType(request.PersonType);
            person.SetName(request.Name);
            person.SetCommemorativeDate(request.CommemorativeDate);
            person.SetEmail(request.Email);
            person.SetProfilePic(request.ProfilePic);
            person.SetDetails(request.Details);
            person.SetRegister(DateTime.Now);

            await _personRepository.SaveChangesAsync();

            return new PersonViewModel()
            {
                ID = person.ID,
                PersonType = person.PersonType,
                Name = person.Name,
                CommemorativeDate = person.CommemorativeDate,
                Email = person.Email,
                ProfilePic = person.ProfilePic,
                Details = person.Details,
                Register = person.Register,
            };

        }
    }
}
