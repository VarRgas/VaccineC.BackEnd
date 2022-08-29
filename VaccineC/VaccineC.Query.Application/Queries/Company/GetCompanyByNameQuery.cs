using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyByNameQuery : IRequest<IEnumerable<CompanyViewModel>>
    {
        public string FantasyName { get; set; }

        public GetCompanyByNameQuery(string name)
        {
            FantasyName = name;
        }
    }
}
