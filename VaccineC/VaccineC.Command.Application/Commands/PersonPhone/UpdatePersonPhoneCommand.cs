using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhone
{
    public class UpdatePersonPhoneCommand : IRequest<IEnumerable<PersonPhoneViewModel>>
    {
        public Guid ID;
        public Guid PersonID;
        public string PhoneType;
        public string NumberPhone;
        public string CodeArea;
        public DateTime Register;

        public UpdatePersonPhoneCommand(Guid id, Guid personId, string phoneType, string numberPhone, string codeArea, DateTime register)
        {
            ID = id;
            PersonID = personId;
            PhoneType = phoneType;
            NumberPhone = numberPhone;
            CodeArea = codeArea;
            Register = register;
        }
    }
}
