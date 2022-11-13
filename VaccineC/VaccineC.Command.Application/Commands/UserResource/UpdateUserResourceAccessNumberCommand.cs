using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class UpdateUserResourceAccessNumberCommand : IRequest
    {
        public Guid UserId;
        public string UrlName;

        public UpdateUserResourceAccessNumberCommand(Guid userId, string urlName)
        {
            UserId = userId;
            UrlName = urlName;
        }
    }
}
