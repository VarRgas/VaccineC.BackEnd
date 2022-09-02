using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonByIdQuery : IRequest<PersonViewModel>
    {
        public Guid Id;

        public GetPersonByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
