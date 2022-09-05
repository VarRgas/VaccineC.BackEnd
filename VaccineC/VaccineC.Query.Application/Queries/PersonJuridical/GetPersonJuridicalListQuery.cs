using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonJuridical
{
    public class GetPersonJuridicalListQuery : IRequest<IEnumerable<PersonsJuridicalViewModel>>
    {
    }
}
