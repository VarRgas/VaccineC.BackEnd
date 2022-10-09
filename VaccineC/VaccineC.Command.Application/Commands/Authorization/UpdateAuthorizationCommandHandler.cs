using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class UpdateAuthorizationCommandHandler : IRequestHandler<UpdateAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;

        public UpdateAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(UpdateAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.ID);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }

            authorization.SetUserId(request.UserId);
            authorization.SetEventId(request.EventId);
            authorization.SetBudgetProductId(request.BudgetProductId);
            authorization.SetBorrowerPersonId(request.BorrowerPersonId);
            authorization.SetSituation(request.Situation);
            authorization.SetTypeOfService(request.TypeOfService);
            authorization.SetNotify(request.Notify);
            authorization.SetAuthorizationDate(request.AuthorizationDate);
            authorization.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return await _appService.GetAllAsync();
        }
    }
}
