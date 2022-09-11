using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementListByMovementNumberQueryHandler : IRequestHandler<GetMovementListByMovementNumberQuery, IEnumerable<MovementViewModel>>
    {
        private readonly IMovementAppService _movementAppService;

        public GetMovementListByMovementNumberQueryHandler(IMovementAppService movementAppService)
        {
            _movementAppService = movementAppService;
        }

        public async Task<IEnumerable<MovementViewModel>> Handle(GetMovementListByMovementNumberQuery request, CancellationToken cancellationToken)
        {

            long n;
            bool isNumeric = long.TryParse(request.Information, out n);

            if (isNumeric)
            {
                int movementNumber = int.Parse(request.Information);
                return await _movementAppService.GetAllByMovementNumber(movementNumber);
            }
            else{
                return await _movementAppService.GetAllByProductName(request.Information);

            }
  
        }
    }
}
