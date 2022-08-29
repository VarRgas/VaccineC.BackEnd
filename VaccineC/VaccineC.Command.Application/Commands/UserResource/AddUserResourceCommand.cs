using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class AddUserResourceCommand : IRequest<UserResourceViewModel>
    {
        public Guid ID;
        public Guid UsersId;
        public Guid ResourcesId;
        public DateTime Register;

        public AddUserResourceCommand(Guid id, Guid usersId, Guid resourcesId, DateTime register)
        {
            ID = id;
            UsersId = usersId;
            ResourcesId = resourcesId;
            Register = register;
        }
    }
}
