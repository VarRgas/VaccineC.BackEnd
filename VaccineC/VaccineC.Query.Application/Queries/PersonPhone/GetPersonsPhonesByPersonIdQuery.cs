using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonsPhonesByPersonIdQuery : IRequest<IEnumerable<PersonPhoneViewModel>>
    {
        public Guid PersonID { get; set; }

        public GetPersonsPhonesByPersonIdQuery(Guid personId)
        {
            PersonID = personId;
        }
    }
}