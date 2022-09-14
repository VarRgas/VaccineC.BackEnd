using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class DeleteProductDosesCommandHandler : IRequestHandler<DeleteProductDosesCommand, Unit>
    {
        private readonly IProductDosesRepository _repository;

        public DeleteProductDosesCommandHandler(IProductDosesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductDosesCommand request, CancellationToken cancellationToken)
        {
            var dose = _repository.GetById(request.Id);
            _repository.Remove(dose);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
