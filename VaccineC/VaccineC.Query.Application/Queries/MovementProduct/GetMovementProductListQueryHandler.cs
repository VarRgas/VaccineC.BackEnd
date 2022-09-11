using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductListQueryHandler : IRequestHandler<GetMovementProductListQuery, IEnumerable<MovementProductViewModel>>
    {

        private readonly IMovementProductAppService _movementProductAppService;

        public GetMovementProductListQueryHandler(IMovementProductAppService movementProductAppService)
        {
            _movementProductAppService = movementProductAppService;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(GetMovementProductListQuery request, CancellationToken cancellationToken)
        {
            return await _movementProductAppService.GetAllAsync();

        }
    }
}
