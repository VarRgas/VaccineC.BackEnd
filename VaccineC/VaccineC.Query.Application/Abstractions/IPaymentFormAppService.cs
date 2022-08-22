using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPaymentFormAppService
    {
        Task<IEnumerable<PaymentFormViewModel>> GetAllAsync();
        PaymentFormViewModel GetById(Guid id);
        Task<IEnumerable<PaymentFormViewModel>> GetByName(string name);

    }
}
