using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IMovementProductAppService
    {
        Task<IEnumerable<MovementProductViewModel>> GetAllAsync();
        Task<IEnumerable<MovementProductViewModel>> GetAllByMovementId(Guid movementId);
    }
}
