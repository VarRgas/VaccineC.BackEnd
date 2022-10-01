using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface IDiscardAppService
    {
        Task<IEnumerable<DiscardViewModel>> GetAllAsync();
        DiscardViewModel GetById(Guid id);
    }
}
