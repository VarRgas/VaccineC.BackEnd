using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Product
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductViewModel>
    {
        private readonly IProductRepository _repository;
        private readonly VaccineCCommandContext _ctx;

        public AddProductCommandHandler(IProductRepository repository, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        public async Task<ProductViewModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Product newProduct = new Domain.Entities.Product(Guid.NewGuid(),
                                                        request.SbimVaccinesId,
                                                        request.Situation,
                                                        request.Details,
                                                        request.SaleValue,
                                                        DateTime.Now,
                                                        request.Name,
                                                        request.MinimumStock

            );

            _repository.Add(newProduct);
            await _repository.SaveChangesAsync();

            return new ProductViewModel()
            {
                ID = newProduct.ID,
                SbimVaccinesId = newProduct.SbimVaccinesId,
                Situation = newProduct.Situation,
                Details = newProduct.Details,
                SaleValue = newProduct.SaleValue,
                Register = newProduct.Register,
                Name = newProduct.Name,
                MinimumStock = newProduct.MinimumStock,
            };

        }

    }
}
