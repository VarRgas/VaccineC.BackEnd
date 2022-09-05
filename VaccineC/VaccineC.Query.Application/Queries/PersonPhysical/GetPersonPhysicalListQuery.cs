using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhysical
{
    public class GetPersonPhysicalListQuery : IRequest<IEnumerable<PersonsPhysicalViewModel>>
    {
    }
}
