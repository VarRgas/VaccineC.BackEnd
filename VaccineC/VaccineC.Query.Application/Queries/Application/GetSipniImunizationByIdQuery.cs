using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetSipniImunizationByIdQuery : IRequest<SipniImunizationViewModel>
    {
        public Guid ApplicationId;

        public GetSipniImunizationByIdQuery(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
