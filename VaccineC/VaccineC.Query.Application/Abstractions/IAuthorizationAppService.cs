using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IAuthorizationAppService
    {
        Task<IEnumerable<AuthorizationViewModel>> GetAllAsync();
        Task<IEnumerable<AuthorizationSummarySituationViewModel>> GetSummarySituationAuthorization();
        AuthorizationViewModel GetById(Guid id);
    }
}
