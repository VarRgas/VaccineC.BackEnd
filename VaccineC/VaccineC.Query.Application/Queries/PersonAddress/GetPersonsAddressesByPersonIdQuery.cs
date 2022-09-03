using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonsAddressesByPersonIdQuery : IRequest<IEnumerable<PersonAddressViewModel>>
    {
        public Guid PersonID { get; set; }

        public GetPersonsAddressesByPersonIdQuery(Guid personId)
        {
            PersonID = personId;
        }
    }
}
