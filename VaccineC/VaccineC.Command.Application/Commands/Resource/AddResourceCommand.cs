using MediatR;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class AddResourceCommand : IRequest<Guid>
    {
        public Guid ID;
        public string Name;
        public string UrlName;
        public DateTime Register;

        public AddResourceCommand(Guid id, string name, string urlName, DateTime register)
        {
            ID = id;
            Name = name;
            UrlName = urlName;
            Register = register;
        }
    }
}
