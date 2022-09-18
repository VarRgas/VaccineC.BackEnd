using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByIdQuery : IRequest<AuthorizationViewModel>
    {
        public Guid Id;

        public GetAuthorizationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
