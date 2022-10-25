using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class AddBudgetCommandHandler : IRequestHandler<AddBudgetCommand, BudgetViewModel>
    {
        private readonly IBudgetRepository _repository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IBudgetAppService _appService;
        private readonly IMediator _mediator;
        private readonly VaccineCContext _context;

        public AddBudgetCommandHandler(IBudgetRepository repository, VaccineCCommandContext ctx, IBudgetAppService appService, IMediator mediator, VaccineCContext context)
        {
            _repository = repository;
            _ctx = ctx;
            _appService = appService;
            _mediator = mediator;
            _context = context;
        }

        public async Task<BudgetViewModel> Handle(AddBudgetCommand request, CancellationToken cancellationToken)
        {

            await validateResponsiblePersonData(request.PersonID);

            Domain.Entities.Budget newBudget = new Domain.Entities.Budget(
                Guid.NewGuid(),
                request.UserID,
                request.PersonID,
                "P",
                0,
                0,
                request.TotalBudgetAmount,
                request.TotalBudgetedAmount,
                request.ExpirationDate,
                request.ApprovalDate,
                request.Details,
                request.BudgetNumber,
                DateTime.Now
                );

            _repository.Add(newBudget);
            await _repository.SaveChangesAsync();

            await addNewBudgetHistoric(newBudget, request.UserID);

            return await _appService.GetById(newBudget.ID);

        }

        private async Task<Unit> validateResponsiblePersonData(Guid personID)
        {
            
            var personResponsible = (from p in _context.Persons
                                  where p.ID.Equals(personID)
                                  select p).FirstOrDefault();


            if (personResponsible == null) {
                throw new ArgumentException("Pessoa não encontrada!");
            }

            if (personResponsible.PersonType.Equals("F")) {
               
                var pfResponsible = (from p in _context.Persons
                                         join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                                         where p.ID.Equals(personID)
                                         select pf).FirstOrDefault();

                if (pfResponsible == null)
                {
                    throw new ArgumentException("O Responsável financeiro não possui informações complementares cadastradas para prosseguir com o Orçamento!");
                }

                if (pfResponsible.CpfNumber.Equals("") || pfResponsible.CpfNumber.Equals(null))
                {
                    throw new ArgumentException("O Responsável financeiro deve possuir um CPF cadastrado para prosseguir com o Orçamento!");
                }
            }
            else
            {
                var pjResponsible = (from p in _context.Persons
                                     join pj in _context.PersonsJuridical on p.ID equals pj.PersonID
                                     where p.ID.Equals(personID)
                                     select pj).FirstOrDefault();

                if (pjResponsible == null)
                {
                    throw new ArgumentException("O Responsável financeiro não possui informações complementares cadastradas para prosseguir com o Orçamento!");
                }

                if (pjResponsible.CnpjNumber.Equals("") || pjResponsible.CnpjNumber.Equals(null))
                {
                    throw new ArgumentException("O Responsável financeiro deve possuir um CNPJ cadastrado para prosseguir com o Orçamento!");
                }
            }

            return Unit.Value;
        }

        public async Task<Unit> addNewBudgetHistoric(Domain.Entities.Budget budget, Guid? userId)
        {

            await _mediator.Send(new AddBudgetHistoricCommand(
                 Guid.NewGuid(),
                 budget.ID,
                 userId,
                 "Orçamento Criado.",
                 DateTime.Now
                 ));

            return Unit.Value;
        }
    }
}
