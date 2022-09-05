using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonJuridical
{
    public class GetPersonJuridicalByPersonIdQuery : IRequest<PersonsJuridicalViewModel>
    {
        public Guid PersonId;

        public GetPersonJuridicalByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
