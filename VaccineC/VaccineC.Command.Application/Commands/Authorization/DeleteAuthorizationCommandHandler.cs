using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class DeleteAuthorizationCommandHandler : IRequestHandler<DeleteAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetProductRepository _budgetProductRepository;

        public DeleteAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator, IEventRepository eventRepository, IBudgetProductRepository budgetProductRepository)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
            _eventRepository = eventRepository;
            _budgetProductRepository = budgetProductRepository; 
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(DeleteAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.Id);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }

            authorization.SetSituation("X");
            await _repository.SaveChangesAsync();

            var eventClass = _eventRepository.GetById(authorization.EventId);

            if (eventClass == null)
            {
                throw new ArgumentException("Evento não encontrado!");
            }

            eventClass.SetSituation("X");
            await _eventRepository.SaveChangesAsync();

            var budgetProduct = _budgetProductRepository.GetById(authorization.BudgetProductId);

            if (budgetProduct == null)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");
            }

            budgetProduct.SetSituationProduct("P");
            await _budgetProductRepository.SaveChangesAsync();

            return await _appService.GetAllAsync();

        }
    }
}
