using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyByNameQuery : IRequest<IEnumerable<CompanyViewModel>>
    {
        public string Name { get; set; }

        public GetCompanyByNameQuery(string name)
        {
            Name = name;
        }
    }
}
