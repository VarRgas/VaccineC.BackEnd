using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class PaymentFormAppService : IPaymentFormAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PaymentFormAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentFormViewModel>> GetAllAsync()
        {
            var paymentForms = await _queryContext.AllPaymentForms.ToListAsync();
            var paymentFormsViewModel = paymentForms.Select(r => _mapper.Map<PaymentFormViewModel>(r)).ToList();
            return paymentFormsViewModel;
        }

        public async Task<IEnumerable<PaymentFormViewModel>> GetByName(String name)
        {

            var paymentForms = await _queryContext.AllPaymentForms.ToListAsync();
            var paymentFormsViewModel = paymentForms
                .Select(r => _mapper.Map<PaymentFormViewModel>(r))
                .Where(r => r.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            return paymentFormsViewModel;

        }

        public PaymentFormViewModel GetById(Guid id)
        {
            var paymentForm = _mapper.Map<PaymentFormViewModel>(_queryContext.AllPaymentForms.Where(r => r.ID == id).First());
            return paymentForm;
        }

    }
}
