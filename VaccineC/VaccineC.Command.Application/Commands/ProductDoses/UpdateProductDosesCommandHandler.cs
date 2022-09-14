using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class UpdateProductDosesCommandHandler : IRequestHandler<UpdateProductDosesCommand, ProductDosesViewModel>
    {
        private readonly IProductDosesRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdateProductDosesCommandHandler(IProductDosesRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<ProductDosesViewModel> Handle(UpdateProductDosesCommand request, CancellationToken cancellationToken)
        {

            var updatedDose = _repository.GetById(request.ID);
            updatedDose.SetProductsId(request.ProductsId);
            updatedDose.SetDoseType(request.DoseType);
            updatedDose.SetDoseRangeMonth(request.DoseRangeMonth);
            updatedDose.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            var doses = await _queryContext.AllProducts.Where(c => c.ID == updatedDose.ID).ToListAsync();
            var dose = doses.Select(r => _mapper.Map<ProductDosesViewModel>(r)).FirstOrDefault();

            return dose;

        }

    }
}
