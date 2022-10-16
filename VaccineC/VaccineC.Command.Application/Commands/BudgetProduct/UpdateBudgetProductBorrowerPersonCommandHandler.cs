using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class UpdateBudgetProductBorrowerPersonCommandHandler : IRequestHandler<UpdateBudgetProductBorrowerPersonCommand, Unit>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;
        private readonly IMediator _mediator;

        public UpdateBudgetProductBorrowerPersonCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateBudgetProductBorrowerPersonCommand request, CancellationToken cancellationToken)
        {
            var updatedBudgetProduct = _repository.GetById(request.ID);

            if (updatedBudgetProduct == null)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");
            }

            updatedBudgetProduct.SetBorrowerPersonId(request.BorrowerPersonId);
            updatedBudgetProduct.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            var budgetProductViewModel = _appService.GetById(updatedBudgetProduct.ID);

            string historic = "O Produto " + budgetProductViewModel.Product.Name + " teve seu tomador alterado para " + budgetProductViewModel.Person.Name + ", através da tela de Agendamentos.";

            await _mediator.Send(new AddBudgetHistoricCommand(
                Guid.NewGuid(),
                updatedBudgetProduct.BudgetId,
                request.UserId,
                historic,
                DateTime.Now
                ));

            return Unit.Value;
        }
    }
}
