using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhysical
{
    public class GetPersonPhysicalByPersonIdQuery : IRequest<PersonsPhysicalViewModel>
    {
        public Guid PersonId;

        public GetPersonPhysicalByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
