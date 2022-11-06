using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Person
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _personRepository.GetById(request.Id);

            if (person == null)
            {
                throw new ArgumentException("Pessoa não encontrada!");
            }

            _personRepository.Remove(person);
            await _personRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
