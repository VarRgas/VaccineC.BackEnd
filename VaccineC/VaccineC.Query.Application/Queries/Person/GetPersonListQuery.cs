using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListQuery : IRequest<IEnumerable<PersonViewModel>>
    {
    }
}
