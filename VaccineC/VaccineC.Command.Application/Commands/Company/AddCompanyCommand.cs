using MediatR;

namespace VaccineC.Command.Application.Commands.Company
{
    public class AddCompanyCommand : IRequest<Guid>
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
