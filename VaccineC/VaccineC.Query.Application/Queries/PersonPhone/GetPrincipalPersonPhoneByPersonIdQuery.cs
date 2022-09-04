using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPrincipalPersonPhoneByPersonIdQuery : IRequest<PersonPhoneViewModel>
    {
        public Guid PersonId;

        public GetPrincipalPersonPhoneByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
