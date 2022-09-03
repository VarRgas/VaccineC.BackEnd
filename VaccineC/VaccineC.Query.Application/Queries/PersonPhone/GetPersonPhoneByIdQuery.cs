using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneByIdQuery : IRequest<PersonPhoneViewModel>
    {
        public Guid Id;

        public GetPersonPhoneByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
