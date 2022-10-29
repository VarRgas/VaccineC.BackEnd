using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Application
{
    public class AddSipniImunizationByIdCommand : IRequest<IEnumerable<ApplicationHistoryViewModel>>
    {
        public Guid ApplicationId;
        public Guid PersonId;

        public AddSipniImunizationByIdCommand(Guid applicationId, Guid personId)
        {
            ApplicationId = applicationId;
            PersonId = personId;
        }
    }
}
