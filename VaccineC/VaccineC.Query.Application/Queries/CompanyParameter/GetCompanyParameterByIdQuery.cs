using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanyParameter
{
    public class GetCompanyParameterByIdQuery : IRequest<CompaniesParametersViewModel>
    {
        public Guid Id;

        public GetCompanyParameterByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
