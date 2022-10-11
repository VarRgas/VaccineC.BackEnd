using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneCelListQuery : IRequest<IEnumerable<PersonPhoneViewModel>>
    {
        public Guid PersonID { get; set; }

        public GetPersonPhoneCelListQuery(Guid personId)
        {
            PersonID = personId;
        }
    }
}
