using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class DeletePhysicalComplementsByIdCommandHandler : IRequestHandler<DeletePhysicalComplementsByIdCommand, Unit>
    {
        private readonly IPersonPhysicalRepository _personPhysicalRepository;

        public DeletePhysicalComplementsByIdCommandHandler(IPersonPhysicalRepository personPhysicalRepository)
        {
            _personPhysicalRepository = personPhysicalRepository;
        }

        public async Task<Unit> Handle(DeletePhysicalComplementsByIdCommand request, CancellationToken cancellationToken)
        {
            var personPhysical = _personPhysicalRepository.GetById(request.Id);
            _personPhysicalRepository.Remove(personPhysical);
            await _personPhysicalRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
