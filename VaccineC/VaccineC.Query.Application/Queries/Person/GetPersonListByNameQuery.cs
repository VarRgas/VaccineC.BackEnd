using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListByNameQuery : IRequest<IEnumerable<PersonViewModel>>
    {
        public string Name { get; set; }

        public GetPersonListByNameQuery(string name)
        {
            Name = name;
        }
    }
}
