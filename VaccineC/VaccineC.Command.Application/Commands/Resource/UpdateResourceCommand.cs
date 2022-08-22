using MediatR;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class UpdateResourceCommand : IRequest<Guid>
    {
        public Guid ID;
        public string Name;
        public string UrlName;
        public DateTime Register;

        public UpdateResourceCommand(Guid id, string name, string urlName, DateTime register)
        {
            ID = id;
            Name = name;
            UrlName = urlName;
            Register = register;
        }
    }
}
