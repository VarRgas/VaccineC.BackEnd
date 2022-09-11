using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductViewModel>
    {
        private readonly IProductRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var updatedProduct = _repository.GetById(request.ID);
            updatedProduct.SetSbimVaccinesId(request.SbimVaccinesId);
            updatedProduct.SetSituation(request.Situation);
            updatedProduct.SetDetails(request.Details);
            updatedProduct.SetSaleValue(request.SaleValue);
            updatedProduct.SetRegister(DateTime.Now);
            updatedProduct.SetName(request.Name);
            updatedProduct.SetMinimumStock(request.MinimumStock);

            await _repository.SaveChangesAsync();

            var products = await _queryContext.AllProducts.Where(c => c.ID == updatedProduct.ID).ToListAsync();
            var product = products.Select(r => _mapper.Map<ProductViewModel>(r)).FirstOrDefault();

            return product;

        }

    }
}
