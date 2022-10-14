using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class SuggestDosesCommandHandler : IRequestHandler<SuggestDosesCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly IBudgetProductAppService _budgetProductAppService;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public SuggestDosesCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IBudgetProductAppService budgetProductAppService, IQueryContext queryContext, IMapper mapper)
        {
            _repository = repository;
            _appService = appService;
            _budgetProductAppService = budgetProductAppService;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(SuggestDosesCommand request, CancellationToken cancellationToken)
        {

            List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel = request.ListAuthorizationSuggestionViewModel;

            await validateDoses(listAuthorizationSuggestionViewModel);

            var firstBudgetProduct = listAuthorizationSuggestionViewModel[0];
            var dateFormated = TimeZoneInfo.ConvertTime(firstBudgetProduct.StartDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            listAuthorizationSuggestionViewModel.RemoveAt(0);

            await validateStartDate(firstBudgetProduct.StartDate);
            await validateDifferentProducts(firstBudgetProduct, listAuthorizationSuggestionViewModel);

            if (firstBudgetProduct.DoseType.Equals("DU") || firstBudgetProduct.DoseType.Equals("DR"))
            {
                return await _budgetProductAppService.GetAllPendingBudgetsProductsByBorrower(firstBudgetProduct.BudgetId, firstBudgetProduct.BorrowerId, dateFormated);
            }

            var clonedListAuthorizationSuggestionViewModel = new List<AuthorizationSuggestionViewModel>(listAuthorizationSuggestionViewModel);

            foreach (var budgetProduct in clonedListAuthorizationSuggestionViewModel)
            {
                if (budgetProduct.DoseType.Equals(firstBudgetProduct.DoseType)) {
                    listAuthorizationSuggestionViewModel.Remove(budgetProduct);
                }
            }

            List<ProductDosesViewModel> productDoseslist = await getProductDosesByProductId(firstBudgetProduct.ProductId);

            if (firstBudgetProduct.DoseType.Equals("D1"))
            {

                int secondInterval = 0;
                int thirdInterval = 0;
                int fourthInterval = 0;
                int fifthInterval = 0;

                foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
                {
                    if (budgetProduct.DoseType.Equals("D2")) {
                        secondInterval = getProductDoseTypeInterval("D2", productDoseslist);
                        if(secondInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(secondInterval);
                        }
                    }else if (budgetProduct.DoseType.Equals("D3"))
                    {
                        thirdInterval = getProductDoseTypeInterval("D3", productDoseslist);
                        if (thirdInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(secondInterval + thirdInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("D4"))
                    {
                        fourthInterval = getProductDoseTypeInterval("D4", productDoseslist);
                        if (fourthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(secondInterval + thirdInterval + fourthInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("DR"))
                    {
                        fifthInterval = getProductDoseTypeInterval("DR", productDoseslist);
                        if (fifthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(secondInterval + thirdInterval + fourthInterval + fifthInterval);
                        }
                    }
                }

            }
            else if (firstBudgetProduct.DoseType.Equals("D2"))
            {

                int thirdInterval = 0;
                int fourthInterval = 0;
                int fifthInterval = 0;

                foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
                {
                    if (budgetProduct.DoseType.Equals("D3"))
                    {
                        thirdInterval = getProductDoseTypeInterval("D3", productDoseslist);
                        if (thirdInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(thirdInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("D4"))
                    {
                        fourthInterval = getProductDoseTypeInterval("D4", productDoseslist);
                        if (fourthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(thirdInterval + fourthInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("DR"))
                    {
                        fifthInterval = getProductDoseTypeInterval("DR", productDoseslist);
                        if (fifthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(thirdInterval + fourthInterval + fifthInterval);
                        }
                    }
                }
            }
            else if (firstBudgetProduct.DoseType.Equals("D3"))
            {

                int fourthInterval = 0;
                int fifthInterval = 0;

                foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
                {
                    if (budgetProduct.DoseType.Equals("D4"))
                    {
                        fourthInterval = getProductDoseTypeInterval("D4", productDoseslist);
                        if (fourthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(fourthInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("DR"))
                    {
                        fifthInterval = getProductDoseTypeInterval("DR", productDoseslist);
                        if (fifthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(fourthInterval + fifthInterval);
                        }
                    }
                }
            }
            else if (firstBudgetProduct.DoseType.Equals("D4"))
            {

                int fifthInterval = 0;

                foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
                {
                    if (budgetProduct.DoseType.Equals("DR"))
                    {
                        fifthInterval = getProductDoseTypeInterval("DR", productDoseslist);
                        if (fifthInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(fifthInterval);
                        }
                    }
                }
            }

            var budgetsProducts = await _queryContext.AllBudgetsProducts.Where(bp => bp.BudgetId == firstBudgetProduct.BudgetId && bp.BorrowerPersonId == firstBudgetProduct.BorrowerId && bp.SituationProduct.Equals("P")).ToListAsync();
            var budgetsProductsViewModel = budgetsProducts.Select(r => _mapper.Map<BudgetProductViewModel>(r)).ToList();

            if (budgetsProductsViewModel.Count() > 0)
            {
                budgetsProductsViewModel[0].ApplicationDate = dateFormated;
            }

            foreach (var budgetProductViewModel in budgetsProductsViewModel)
            {
                foreach (var budgetProductEdited in listAuthorizationSuggestionViewModel)
                {
                    if (budgetProductViewModel.ID.Equals(budgetProductEdited.BudgetProductId))
                    {
                        budgetProductViewModel.ApplicationDate = budgetProductEdited.ApplicationDate;
                    }
                }

            }

            return budgetsProductsViewModel;
        }

        private async Task<Unit> validateDifferentProducts(AuthorizationSuggestionViewModel firstBudgetProduct, List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
            {
                if (budgetProduct.ProductId != firstBudgetProduct.ProductId)
                {
                    throw new ArgumentException("Não é possível sugerir datas para Produtos diferentes!");
                }
            }

            return Unit.Value;
        }

        private int getProductDoseTypeInterval(string interval, List<ProductDosesViewModel> productDoseslist)
        {
            foreach (var productDose in productDoseslist)
            {
                if (productDose.DoseType.Equals(interval)) {
                    return (int)productDose.DoseRangeMonth;
                }
            }

            return 0;
        }

        private async Task<List<ProductDosesViewModel>> getProductDosesByProductId(Guid productId)
        {
            var productsDoses = await _queryContext.AllProductsDoses.ToListAsync();
            var productsDosesViewModel = productsDoses
                .Select(r => _mapper.Map<ProductDosesViewModel>(r))
                .Where(r => r.ProductsId == productId)
                .OrderBy(r => r.DoseType)
                .ToList();
            return productsDosesViewModel;
        }

        private async Task<Unit> validateDoses(List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            if (listAuthorizationSuggestionViewModel == null || listAuthorizationSuggestionViewModel.Count() <= 1)
            {
                throw new ArgumentException("É necessário selecionar 2 ou mais produtos para a sugestão de doses!");
            }

            foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
            {

                if (budgetProduct.DoseType.Equals(""))
                {
                    throw new ArgumentException("Produto(s) sem Doses configuradas!");
                }
            }

            return Unit.Value;
        }

        private async Task<Unit> validateStartDate(DateTime startDate)
        {

            DateTime before = new DateTime(2010, 01, 01);
            DateTime after = new DateTime(2055, 01, 01);

            if (startDate == null)
            {
                throw new ArgumentException("Data informada inválida, verifique!");
            }

            if (startDate <= before)
            {
                throw new ArgumentException("Data informada inválida, verifique!");
            }

            if (startDate >= after)
            {
                throw new ArgumentException("Data informada inválida, verifique!");
            }

            return Unit.Value;
        }
    }
}
