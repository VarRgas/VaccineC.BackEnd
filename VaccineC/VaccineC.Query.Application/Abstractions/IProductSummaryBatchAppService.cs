using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IProductSummaryBatchAppService
    {
        Task<IEnumerable<ProductSummaryBatchViewModel>> GetAllAsync();
        Task<IEnumerable<ProductSummaryBatchViewModel>> GetProductSummaryBatchByProductId(Guid productsId);
        Task<IEnumerable<ProductSummaryBatchViewModel>> GetValidProductSummaryBatchList(Guid productsId);
        Task<IEnumerable<ProductSummaryBatchViewModel>> GetAllBelowMinimumStockAsync();
        ProductSummaryBatchViewModel GetById(Guid id);
        Task<ProductSummaryBatchViewModel> GetByName(Guid id, string name);
    }
}
