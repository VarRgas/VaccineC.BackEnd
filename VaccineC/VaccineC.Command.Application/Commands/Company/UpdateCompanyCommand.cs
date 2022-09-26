using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Company
{
    public class UpdateCompanyCommand : IRequest<CompanyViewModel>
    {
        public Guid ID;
        public Guid PersonId;
        public string Details;
        public DateTime Register;

        public UpdateCompanyCommand(Guid id, Guid personId, string details, DateTime register)
        {
            ID = id;
            PersonId = personId;
            Details = details;
            Register = register;
        }
    }
}
