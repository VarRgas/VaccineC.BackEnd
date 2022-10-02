using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class AddBudgetNegotiationCommandHandler : IRequestHandler<AddBudgetNegotiationCommand, IEnumerable<BudgetNegotiationViewModel>>
    {
        private readonly IBudgetNegotiationRepository _repository;
        private readonly IPaymentFormRepository _paymentFormRepository;
        private readonly IBudgetNegotiationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;

        public AddBudgetNegotiationCommandHandler(IBudgetNegotiationRepository repository, IPaymentFormRepository paymentFormRepository, IBudgetNegotiationAppService appService, VaccineCCommandContext ctx, IMediator mediator)
        {
            _repository = repository;
            _paymentFormRepository = paymentFormRepository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(AddBudgetNegotiationCommand request, CancellationToken cancellationToken)
        {

            await validadeTotalAmountBalance(request.TotalAmountBalance, request.TotalAmountTraded);
            await validateInstallments(request.Installments);
            await validateMaximumInstallments(request.PaymentFormId, request.Installments);

            Domain.Entities.BudgetNegotiation newBudgetNegotiation = new Domain.Entities.BudgetNegotiation(
                Guid.NewGuid(),
                request.BudgetId,
                request.PaymentFormId,
                request.TotalAmountBalance,
                request.TotalAmountTraded,
                request.Installments,
                DateTime.Now
                );

            _repository.Add(newBudgetNegotiation);
            await _repository.SaveChangesAsync();

            await addNewBudgetHistoric(newBudgetNegotiation, request.UserId);

            return await _appService.GetAllBudgetsNegotiationsByBudgetId(newBudgetNegotiation.BudgetId);
        }

        public async Task<Unit> validateInstallments(int installments)
        {

            if (installments == null || installments == 0)
            {
                throw new ArgumentException("O Nº de Parcelas deve ser maio que 0!");
            }

            return Unit.Value;

        }

        public async Task<Unit> validateMaximumInstallments(Guid paymentFormId, int installments)
        {
            var paymentForm = _paymentFormRepository.GetById(paymentFormId);

            if(paymentForm == null)
            {
                throw new ArgumentException("Forma de Pagamento não encontrada!");
            }

            if (paymentForm.MaximumInstallments < installments) {
                throw new ArgumentException("O nº máximo de parcelas para a forma de pagamento " + paymentForm.Name + " é de " + paymentForm.MaximumInstallments + "!");
            }

            return Unit.Value;
        }

        public async Task<Unit> validadeTotalAmountBalance(decimal totalAmountBalance, decimal totalAmountTraded)
        {
            if (totalAmountTraded <= 0) {
                throw new ArgumentException("O R$ Negociado precisa ser maior que 0!");
            }
            else if (totalAmountBalance <= 0)
            {
                throw new ArgumentException("A negociação já está completa!");
            }
            else if (totalAmountBalance < totalAmountTraded)
            {
                throw new ArgumentException("O valor da negociação não pode ser maior do que o saldo restante a ser negociado!");
            }

            return Unit.Value;
        }

        public async Task<Unit> addNewBudgetHistoric(Domain.Entities.BudgetNegotiation budgetNegotiation, Guid? userId)
        {
            var budgetProductViewModel = _appService.GetById(budgetNegotiation.ID);

            string historic = "Adicionada nova negociação ao orçamento: " + budgetProductViewModel.Installments + "x de R$" + budgetProductViewModel.TotalAmountTraded + " - " + budgetProductViewModel.PaymentForm.Name + ".";

            await _mediator.Send(new AddBudgetHistoricCommand(
                 Guid.NewGuid(),
                 budgetProductViewModel.BudgetId,
                 userId,
                 historic,
                 DateTime.Now
                 ));

            return Unit.Value;
        }
    }
}
