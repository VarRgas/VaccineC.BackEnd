using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IMovementAppService
    {
        Task<IEnumerable<MovementViewModel>> GetAllAsync();
        Task<IEnumerable<MovementViewModel>> GetAllByMovementNumber(int movementNumber);
        Task<IEnumerable<MovementViewModel>> GetAllByProductName(string productName);
    }
}
