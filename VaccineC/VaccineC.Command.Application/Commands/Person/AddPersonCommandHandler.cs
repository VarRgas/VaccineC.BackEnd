using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Person
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, PersonViewModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddPersonCommandHandler(IPersonRepository personRepository, VaccineCCommandContext ctx)
        {
            _personRepository = personRepository;
            _ctx = ctx;
        }

        public async Task<PersonViewModel> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Person newPerson = new Domain.Entities.Person(Guid.NewGuid(),
                                                                          request.PersonType,
                                                                          request.Name,
                                                                          request.CommemorativeDate,
                                                                          request.Email,
                                                                          request.ProfilePic,
                                                                          request.Details,
                                                                          DateTime.Now
            );

            _personRepository.Add(newPerson);
            await _personRepository.SaveChangesAsync();

            return new PersonViewModel()
            {
                ID = newPerson.ID,
                PersonType = newPerson.PersonType,
                Name = newPerson.Name,
                CommemorativeDate = newPerson.CommemorativeDate,
                Email = newPerson.Email,
                ProfilePic = newPerson.ProfilePic,
                Details = newPerson.Details,
                Register = newPerson.Register,
            };

        }

    }
}
