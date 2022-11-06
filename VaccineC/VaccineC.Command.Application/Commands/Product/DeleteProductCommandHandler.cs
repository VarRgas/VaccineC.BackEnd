using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetById(request.Id);

            if (product == null)
            {
                throw new ArgumentException("Produto não encontrado!");
            }

            _repository.Remove(product);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
