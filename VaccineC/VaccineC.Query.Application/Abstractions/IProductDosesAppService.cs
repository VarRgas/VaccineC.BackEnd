using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IProductDosesAppService
    {
        Task<IEnumerable<ProductDosesViewModel>> GetAllAsync();
        Task<IEnumerable<ProductDosesViewModel>> GetByType(String type);
        ProductDosesViewModel GetById(Guid id);
    }
}
