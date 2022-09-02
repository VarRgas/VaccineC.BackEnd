using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Person
{
    public class AddPersonCommand : IRequest<PersonViewModel>
    {
        public Guid ID;
        public string PersonType;
        public string Name;
        public DateTime? CommemorativeDate;
        public string Email;
        public string? ProfilePic;
        public string? Details;
        public DateTime Register;

        public AddPersonCommand(Guid id, string personType, string name, DateTime? commemorativeDate, string email, string? profilePic, string? details, DateTime register)
        {
            ID = id;
            PersonType = personType;
            Name = name;
            CommemorativeDate = commemorativeDate;
            Email = email;
            ProfilePic = profilePic;
            Details = details;
            Register = register;
        }
    }
}
