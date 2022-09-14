using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class AddProductDosesCommandHandler : IRequestHandler<AddProductDosesCommand, IEnumerable<ProductDosesViewModel>>
    {
        private readonly IProductDosesRepository _repository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IProductDosesAppService _appService;

        public AddProductDosesCommandHandler(IProductDosesRepository repository, VaccineCCommandContext ctx, IProductDosesAppService service)
        {
            _repository = repository;
            _ctx = ctx;
            _appService = service;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(AddProductDosesCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.ProductDoses newDose = new Domain.Entities.ProductDoses(Guid.NewGuid(),
                                                        request.ProductsId,
                                                        request.DoseType,
                                                        request.DoseRangeMonth,
                                                        DateTime.Now

            );

            _repository.Add(newDose);
            await _repository.SaveChangesAsync();

            return await _appService.GetProductsDosesByProductId(newDose.ProductsId);

        }

    }
}
