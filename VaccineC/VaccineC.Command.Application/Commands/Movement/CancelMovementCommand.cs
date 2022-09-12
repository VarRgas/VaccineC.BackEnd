using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class CancelMovementCommand : IRequest<MovementViewModel>
    {
        public Guid ID;
        public int MovementNumber;
        public Guid UsersId;
        public string MovementType;
        public decimal? ProductsAmount;
        public DateTime Register;
        public string Situation;

        public CancelMovementCommand(Guid id, int movementNumber, Guid usersId, string movementType, decimal? productsAmount, DateTime register, string situation)
        {
            ID = id;
            MovementNumber = movementNumber;
            UsersId = usersId;
            MovementType = movementType;
            ProductsAmount = productsAmount;
            Register = register;
            Situation = situation;
        }
    }
}
