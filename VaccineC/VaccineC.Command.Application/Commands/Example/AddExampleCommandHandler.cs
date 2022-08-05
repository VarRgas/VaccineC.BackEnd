using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Example
{
    public class AddExampleCommandHandler : IRequestHandler<AddExampleCommand, Unit>
    {
        private readonly IExampleRepository _exampleRepository;

        public AddExampleCommandHandler(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public async Task<Unit> Handle(AddExampleCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Example newCustomer = new Domain.Entities.Example(request.Name, request.Phone, request.CPF, request.Email);
            _exampleRepository.Add(newCustomer);
            await _exampleRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
