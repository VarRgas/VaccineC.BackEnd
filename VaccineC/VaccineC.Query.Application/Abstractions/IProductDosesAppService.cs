using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IProductDosesAppService
    {
        Task<IEnumerable<ProductDosesViewModel>> GetAllAsync();
        Task<IEnumerable<ProductDosesViewModel>> GetProductsDosesByProductId(Guid productsId);
        ProductDosesViewModel GetById(Guid id);
    }
}
