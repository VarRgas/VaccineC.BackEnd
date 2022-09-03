using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonAddressListQuery : IRequest<IEnumerable<PersonAddressViewModel>>
    {

    }
}
