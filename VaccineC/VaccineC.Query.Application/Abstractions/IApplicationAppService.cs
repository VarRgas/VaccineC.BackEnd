using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IApplicationAppService
    {
        Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
        Task<IEnumerable<ApplicationViewModel>> GetByName(String name);
        Task<IEnumerable<ApplicationAvailableViewModel>> GetAvailableApplicationsByPersonId(Guid personId);
        Task<IEnumerable<ApplicationHistoryViewModel>> GetHistoryApplicationsByPersonId(Guid personId);
        Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(string personName, DateTime applicationDate, Guid responsibleId);
        Task<bool> GetPersonApplicationProductSameDay(Guid personId, Guid productId);
        Task<int> GetApplicationNumberByPersonId(Guid personId);
        ApplicationViewModel GetById(Guid id);
    }
}
