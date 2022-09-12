using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class FinishMovementCommand : IRequest<MovementViewModel>
    {
        public Guid ID;
        public int MovementNumber;
        public Guid UsersId;
        public string MovementType;
        public decimal? ProductsAmount;
        public DateTime Register;
        public string Situation;

        public FinishMovementCommand(Guid iD, int movementNumber, Guid usersId, string movementType, decimal? productsAmount, DateTime register, string situation)
        {
            ID = iD;
            MovementNumber = movementNumber;
            UsersId = usersId;
            MovementType = movementType;
            ProductsAmount = productsAmount;
            Register = register;
            Situation = situation;
        }
    }
}
