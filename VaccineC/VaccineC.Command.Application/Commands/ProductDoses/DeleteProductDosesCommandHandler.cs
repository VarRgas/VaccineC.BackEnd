using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class DeleteProductDosesCommandHandler : IRequestHandler<DeleteProductDosesCommand, IEnumerable<ProductDosesViewModel>>
    {
        private readonly IProductDosesRepository _repository;
        private readonly IProductDosesAppService _appService;

        public DeleteProductDosesCommandHandler(IProductDosesRepository repository, IProductDosesAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(DeleteProductDosesCommand request, CancellationToken cancellationToken)
        {
            var dose = _repository.GetById(request.Id);

            if (dose == null)
            {
                throw new ArgumentException("Produto Dose não encontrado!");
            }

            _repository.Remove(dose);
            await _repository.SaveChangesAsync();

            return await _appService.GetProductsDosesByProductId(dose.ProductsId);
        }
    }
}
