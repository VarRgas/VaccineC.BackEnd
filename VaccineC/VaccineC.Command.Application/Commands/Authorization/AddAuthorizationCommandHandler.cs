using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class AddAuthorizationCommandHandler : IRequestHandler<AddAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;

        public AddAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(AddAuthorizationCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Authorization newAuthorization = new Domain.Entities.Authorization(
                Guid.NewGuid(),
                request.UserId,
                request.EventId,
                request.BudgetProductId,
                request.BorrowerPersonId,
                request.AuthorizationNumber,
                request.Situation,
                request.TypeOfService,
                request.Notify,
                request.AuthorizationDate,
                DateTime.Now
                );

            _repository.Add(newAuthorization);
            await _repository.SaveChangesAsync();

            return await _appService.GetAllAsync();
        }
    }
}
