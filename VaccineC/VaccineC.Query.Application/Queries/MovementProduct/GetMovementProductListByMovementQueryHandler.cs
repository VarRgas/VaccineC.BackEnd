using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductListByMovementQueryHandler : IRequestHandler<GetMovementProductListByMovementQuery, IEnumerable<MovementProductViewModel>>
    {
        private readonly IMovementProductAppService _movementProductAppService;

        public GetMovementProductListByMovementQueryHandler(IMovementProductAppService movementProductAppService)
        {
            _movementProductAppService = movementProductAppService;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(GetMovementProductListByMovementQuery request, CancellationToken cancellationToken)
        {
            return await _movementProductAppService.GetAllByMovementId(request.MovementId);
        }
    }
}
