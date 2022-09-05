using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommand : IRequest<IEnumerable<PersonsPhysicalViewModel>>
    {
        public Guid ID;
        public Guid PersonID;
        public string MaritalStatus;
        public string Gender;
        public DateTime? DeathDate;
        public DateTime Register;
        public string? CnsNumber;
        public string? CpfNumber;

        public UpdatePhysicalComplementsCommand(Guid id, Guid personId, string maritalStatus, string gender, DateTime? deathDate, DateTime register, string? cnsNumber, string? cpfNumber)
        {
            ID = id;
            PersonID = personId;
            MaritalStatus = maritalStatus;
            Gender = gender;
            DeathDate = deathDate;
            Register = register;
            CnsNumber = cnsNumber;
            CpfNumber = cpfNumber;
        }
    }
}
