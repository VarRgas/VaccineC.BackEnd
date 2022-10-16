using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IAuthorizationAppService
    {
        Task<IEnumerable<AuthorizationViewModel>> GetAllAsync();
        Task<IEnumerable<AuthorizationViewModel>> GetAllByAuthNumber(int authNumber, string situation, Guid responsibleId);
        Task<IEnumerable<AuthorizationViewModel>> GetAllByBorrowerName(string borrowerName, string situation, Guid responsibleId);
        Task<IEnumerable<AuthorizationSummarySituationViewModel>> GetSummarySituationAuthorization();
        AuthorizationViewModel GetById(Guid id);
    }
}
