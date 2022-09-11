using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementListByMovementNumberQuery : IRequest<IEnumerable<MovementViewModel>>
    {
        public string Information { get; set; }

        public GetMovementListByMovementNumberQuery(string information)
        {
            Information = information;
        }
    }
}
