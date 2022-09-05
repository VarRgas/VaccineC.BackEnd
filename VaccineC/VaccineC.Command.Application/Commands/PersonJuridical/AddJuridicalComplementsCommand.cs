using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class AddJuridicalComplementsCommand : IRequest<IEnumerable<PersonsJuridicalViewModel>>
    {
        public Guid ID;
        public Guid PersonID;
        public string FantasyName;
        public string CnpjNumber;
        public DateTime Register;

        public AddJuridicalComplementsCommand(Guid id, Guid personId, string fantasyName, string cnpjNumber, DateTime register)
        {
            ID = id;
            PersonID = personId;
            FantasyName = fantasyName;
            CnpjNumber = cnpjNumber;
            Register = register;
        }
    }
}
