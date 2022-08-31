using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanyParameter
{
    public class GetCompanyParameterListQuery : IRequest<IEnumerable<CompaniesParametersViewModel>>
    {

    }
}
