using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationByPersonNameQuery : IRequest<IEnumerable<ApplicationViewModel>>
    {
        public string Name { get; set; }

        public GetApplicationByPersonNameQuery(string name)
        {
            Name = name;
        }
    }
}
