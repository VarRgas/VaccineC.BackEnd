using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface ILoginAppService
    {
        LoginViewModel GetById(Guid userId);
    }
}
