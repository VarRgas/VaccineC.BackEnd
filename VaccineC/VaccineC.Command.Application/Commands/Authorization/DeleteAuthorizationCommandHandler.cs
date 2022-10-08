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

        public DeleteAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(DeleteAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.Id);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }
            
            _repository.Remove(authorization);
            await _repository.SaveChangesAsync();

            return await _appService.GetAllAsync();

        }
    }
}
