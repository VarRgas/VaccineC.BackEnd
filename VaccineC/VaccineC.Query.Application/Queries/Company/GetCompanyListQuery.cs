using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyListQuery : IRequest<IEnumerable<CompanyViewModel>>
    {

    }
}
