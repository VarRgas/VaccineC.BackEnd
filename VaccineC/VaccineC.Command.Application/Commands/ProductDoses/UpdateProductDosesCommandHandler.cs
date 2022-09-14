using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class UpdateProductDosesCommandHandler : IRequestHandler<UpdateProductDosesCommand, IEnumerable<ProductDosesViewModel>>
    {
        private readonly IProductDosesRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly IProductDosesAppService _appService;

        public UpdateProductDosesCommandHandler(IProductDosesRepository repository, IQueryContext queryContext, IMapper mapper, IProductDosesAppService appService)
        {
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
            _appService = appService;   
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(UpdateProductDosesCommand request, CancellationToken cancellationToken)
        {

            var updatedDose = _repository.GetById(request.ID);
            updatedDose.SetProductsId(request.ProductsId);
            updatedDose.SetDoseType(request.DoseType);
            updatedDose.SetDoseRangeMonth(request.DoseRangeMonth);
            updatedDose.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return await _appService.GetProductsDosesByProductId(updatedDose.ProductsId);
            
        }

    }
}
