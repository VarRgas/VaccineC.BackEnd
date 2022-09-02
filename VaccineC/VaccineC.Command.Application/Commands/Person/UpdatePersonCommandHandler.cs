using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Person
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonViewModel>
    {
        private readonly IPersonRepository _personRepository;

        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonViewModel> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {

            var person = _personRepository.GetById(request.ID);
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
