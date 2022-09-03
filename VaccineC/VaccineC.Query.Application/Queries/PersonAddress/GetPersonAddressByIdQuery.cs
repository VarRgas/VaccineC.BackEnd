using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonAddressByIdQuery : IRequest<PersonAddressViewModel>
    {
        public Guid Id;

        public GetPersonAddressByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
