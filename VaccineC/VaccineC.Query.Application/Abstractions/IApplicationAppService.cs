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
        Task<IEnumerable<ApplicationPersonGenderViewModel>> GetApplicationsByPersonGender(int month, int year);
        Task<IEnumerable<ApplicationPersonAgeViewModel>> GetApplicationsByPersonAge(int month, int year);
        Task<IEnumerable<ApplicationTypeViewModel>> GetApplicationsByType(int month, int year);
        Task<IEnumerable<ApplicationSipniIntegrationViewModel>> GetSipniIntegrationSituation(int month, int year);
        Task<IEnumerable<ApplicationProductViewModel>> GetApplicationsByProductId(int month, int year);
        Task <ApplicationNumberViewModel> GetApplicationsNumbers(int month, int year);
        Task<bool> GetPersonApplicationProductSameDay(Guid personId, Guid productId);
        Task<int> GetApplicationNumberByPersonId(Guid personId);
        Task<bool> VerifyApplicationAbleUpdate(Guid applicationId, Guid userId);
        ApplicationViewModel GetById(Guid id);
    }
}
