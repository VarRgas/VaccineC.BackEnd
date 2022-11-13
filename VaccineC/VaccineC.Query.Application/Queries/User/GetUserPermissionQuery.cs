using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserPermissionQuery : IRequest<Boolean>
    {
        public Guid Id;
        public string CurrentUrl;

        public GetUserPermissionQuery(Guid id, string currentUrl)
        {
            Id = id;
            CurrentUrl = currentUrl;
        }
    }
}
