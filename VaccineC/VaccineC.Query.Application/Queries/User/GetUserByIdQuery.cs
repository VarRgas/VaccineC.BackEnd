using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public Guid Id;

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
