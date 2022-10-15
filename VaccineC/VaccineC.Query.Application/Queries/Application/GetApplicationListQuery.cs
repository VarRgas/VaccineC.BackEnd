using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationListQuery : IRequest<IEnumerable<ApplicationViewModel>>
    {
    }
}
