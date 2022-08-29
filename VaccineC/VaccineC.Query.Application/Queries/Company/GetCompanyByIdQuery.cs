using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyByIdQuery : IRequest<CompanyViewModel>
    {
        public Guid Id;

        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
