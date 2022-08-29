using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceByIdQuery : IRequest<UserResourceViewModel>
    {
        public Guid Id;

        public GetUserResourceByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
