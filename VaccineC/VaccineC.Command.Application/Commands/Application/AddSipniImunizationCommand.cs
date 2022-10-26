using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Command.Application.Commands.Application
{
    public class AddSipniImunizationCommand : IRequest
    {
        public Domain.Entities.Application Application;

        public AddSipniImunizationCommand(Domain.Entities.Application application)
        {
            Application = application;
        }
    }
}
