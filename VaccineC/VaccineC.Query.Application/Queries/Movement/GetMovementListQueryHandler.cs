using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementListQueryHandler : IRequestHandler<GetMovementListQuery, IEnumerable<MovementViewModel>>
    {

        private readonly IMovementAppService _movementAppService;

        public GetMovementListQueryHandler(IMovementAppService movementAppService)
        {
            _movementAppService = movementAppService;
        }

        public async Task<IEnumerable<MovementViewModel>> Handle(GetMovementListQuery request, CancellationToken cancellationToken)
        {
            return await _movementAppService.GetAllAsync();
        }
    }
}
