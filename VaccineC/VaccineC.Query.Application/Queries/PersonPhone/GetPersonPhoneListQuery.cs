using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneListQuery : IRequest<IEnumerable<PersonPhoneViewModel>>
    {

    }
}
