using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class UpdateUserResourceCommand : IRequest<IEnumerable<ResourceViewModel>>
    {
        public Guid ID;
        public Guid UsersId;
        public Guid ResourcesId;
        public DateTime Register;

        public UpdateUserResourceCommand(Guid id, Guid usersId, Guid resourcesId, DateTime register)
        {
            ID = id;
            UsersId = usersId;
            ResourcesId = resourcesId;
            Register = register;
        }
    
    }
}
