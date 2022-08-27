using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListByTypeQuery : IRequest<IEnumerable<PersonViewModel>>
    {
        public string PersonType { get; set; }

        public GetPersonListByTypeQuery(string personType)
        {
            PersonType = personType;
        }
    }
}
