using MediatR;

namespace VaccineC.Command.Application.Commands.Company
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid Id;

        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }
    }
}
