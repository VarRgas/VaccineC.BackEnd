using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<IEnumerable<ProductViewModel>> GetByName(String name);
        ProductViewModel GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAllProductsVaccinesAutocomplete();
    }
}
