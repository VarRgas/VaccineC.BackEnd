using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Company
{
    public class AddCompanyCommand : IRequest<CompanyViewModel>
    {
        public Guid ID;
        public Guid PersonID { get; set; }
        public string Details;
        public DateTime Register;
        public AddCompanyCommand(Guid id, Guid personID, string details, DateTime register)
        {
            ID = id;
            PersonID = personID;
            Details = details;
            Register = register;
        }
    }
}
