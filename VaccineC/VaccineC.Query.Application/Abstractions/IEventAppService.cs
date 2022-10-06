using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface IEventAppService
    {
        Task<IEnumerable<EventViewModel>> GetAllAsync();
        EventViewModel GetById(Guid id);
    }
}
