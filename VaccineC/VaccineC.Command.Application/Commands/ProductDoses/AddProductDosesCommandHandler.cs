using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class AddProductDosesCommandHandler : IRequestHandler<AddProductDosesCommand, ProductDosesViewModel>
    {
        private readonly IProductDosesRepository _repository;
        private readonly VaccineCCommandContext _ctx;

        public AddProductDosesCommandHandler(IProductDosesRepository repository, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        public async Task<ProductDosesViewModel> Handle(AddProductDosesCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.ProductDoses newDose = new Domain.Entities.ProductDoses(Guid.NewGuid(),
                                                        request.ProductsId,
                                                        request.DoseType,
                                                        request.DoseRangeMonth,
                                                        DateTime.Now

            );

            _repository.Add(newDose);
            await _repository.SaveChangesAsync();

            return new ProductDosesViewModel()
            {
                ID = newDose.ID,
                ProductsId = newDose.ProductsId,
                DoseType = newDose.DoseType,
                DoseRangeMonth = newDose.DoseRangeMonth,
                Register = newDose.Register
            };

        }

    }
}
