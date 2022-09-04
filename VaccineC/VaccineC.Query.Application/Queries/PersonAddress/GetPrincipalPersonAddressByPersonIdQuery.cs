using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPrincipalPersonAddressByPersonIdQuery : IRequest<PersonAddressViewModel>
    {
        public Guid PersonId;

        public GetPrincipalPersonAddressByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        } 
    }
}
