using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class VerifyApplicationAbleUpdateQuery : IRequest<Boolean>
    {
        public Guid ApplicationId;
        public Guid UserId;

        public VerifyApplicationAbleUpdateQuery(Guid applicationId, Guid userId)
        {
            ApplicationId = applicationId;
            UserId = userId;
        }
    }
}
