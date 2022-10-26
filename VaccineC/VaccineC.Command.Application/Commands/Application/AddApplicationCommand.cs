using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Application
{
    public class AddApplicationCommand : IRequest<IEnumerable<ApplicationAvailableViewModel>>
    {
        public Guid ID;
        public Guid UserId;
        public Guid? BudgetProductId;
        public DateTime? ApplicationDate;
        public string DoseType;
        public string RouteOfAdministration;
        public string ApplicationPlace;
        public DateTime? Register;
        public Guid? ProductSummaryBatchId;
        public Guid AuthorizationId;
        public string? SipniIntegrationId;

        public AddApplicationCommand(Guid id, Guid userId, Guid? budgetProductId, DateTime? applicationDate, string doseType, string routeOfAdministration, string applicationPlace, DateTime? register, Guid? productSummaryBatchId, Guid authorizationId, string? sipniIntegrationId)
        {
            ID = id;
            UserId = userId;
            BudgetProductId = budgetProductId;
            ApplicationDate = applicationDate;
            DoseType = doseType;
            RouteOfAdministration = routeOfAdministration;
            ApplicationPlace = applicationPlace;
            Register = register;
            ProductSummaryBatchId = productSummaryBatchId;
            AuthorizationId = authorizationId;
            SipniIntegrationId = sipniIntegrationId;
        }
    }
}
