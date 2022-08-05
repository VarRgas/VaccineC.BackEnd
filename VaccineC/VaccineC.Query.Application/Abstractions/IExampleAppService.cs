using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IExampleAppService
    {
        Task<IEnumerable<ExampleViewModel>> GetAllAsync();
        ExampleViewModel GetById(int id);
    }
}
