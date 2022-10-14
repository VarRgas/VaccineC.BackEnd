using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.Queries.CompanyParameter;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class SuggestJuridicalDosesCommandHandler : IRequestHandler<SuggestJuridicalDosesCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly IBudgetProductAppService _budgetProductAppService;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SuggestJuridicalDosesCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IBudgetProductAppService budgetProductAppService, IQueryContext queryContext, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _budgetProductAppService = budgetProductAppService;
            _queryContext = queryContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(SuggestJuridicalDosesCommand request, CancellationToken cancellationToken)
        {
            List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel = request.ListAuthorizationSuggestionViewModel;

            if (listAuthorizationSuggestionViewModel == null || listAuthorizationSuggestionViewModel.Count() <= 1)
            {
                throw new ArgumentException("É necessário selecionar 2 ou mais produtos para a sugestão de doses!");
            }

            var firstBudgetProduct = listAuthorizationSuggestionViewModel[0];
            listAuthorizationSuggestionViewModel.RemoveAt(0);

            Boolean isDifferentBorrower = false;

            foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
            {
                if (budgetProduct.BorrowerId != firstBudgetProduct.BorrowerId)
                {
                    isDifferentBorrower = true;
                }
            }


            if (isDifferentBorrower)
            {
               return await suggestDosesSeveralBorrowers(firstBudgetProduct, listAuthorizationSuggestionViewModel);
            }
            else
            {
                return await suggestDosesSameBorrower(firstBudgetProduct, listAuthorizationSuggestionViewModel);
            }


        }

        private async Task<IEnumerable<BudgetProductViewModel>> suggestDosesSameBorrower(AuthorizationSuggestionViewModel firstBudgetProduct, List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {

            var dateFormated = TimeZoneInfo.ConvertTime(firstBudgetProduct.StartDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
            {
                if (budgetProduct.ProductId != firstBudgetProduct.ProductId)
                {
                    throw new ArgumentException("Não é possível sugerir datas para Produtos diferentes!");
                }
            }

            if (firstBudgetProduct.DoseType.Equals("DU") || firstBudgetProduct.DoseType.Equals("DR"))
            {
                return await _budgetProductAppService.GetAllPendingBudgetsProductsByBorrower(firstBudgetProduct.BudgetId, firstBudgetProduct.BorrowerId, dateFormated);
            }

            var clonedListAuthorizationSuggestionViewModel = new List<AuthorizationSuggestionViewModel>(listAuthorizationSuggestionViewModel);

            foreach (var budgetProduct in clonedListAuthorizationSuggestionViewModel)
            {
                if (budgetProduct.DoseType.Equals(firstBudgetProduct.DoseType))
                {
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
                    if (budgetProduct.DoseType.Equals("D2"))
                    {
                        secondInterval = getProductDoseTypeInterval("D2", productDoseslist);
                        if (secondInterval != 0)
                        {
                            budgetProduct.ApplicationDate = dateFormated.AddMonths(secondInterval);
                        }
                    }
                    else if (budgetProduct.DoseType.Equals("D3"))
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

        private async Task<IEnumerable<BudgetProductViewModel>> suggestDosesSeveralBorrowers(AuthorizationSuggestionViewModel firstBudgetProduct, List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            var dateFormated = TimeZoneInfo.ConvertTime(firstBudgetProduct.StartDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var companyParameter = await getCompanyParameter();

            if (companyParameter == null) 
            {
                throw new ArgumentException("Parâmetros não encontrados!");
            }

            int applicationTimePerMinuteAux = companyParameter.ApplicationTimePerMinute;

            foreach (var budgetProduct in listAuthorizationSuggestionViewModel)
            {
                budgetProduct.ApplicationDate = dateFormated.AddMinutes(applicationTimePerMinuteAux);
                applicationTimePerMinuteAux = applicationTimePerMinuteAux + companyParameter.ApplicationTimePerMinute; 
            }

            var budgetsProducts = await _queryContext.AllBudgetsProducts.Where(bp => bp.BudgetId == firstBudgetProduct.BudgetId && bp.SituationProduct.Equals("P")).ToListAsync();
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

        private async Task<CompaniesParametersViewModel> getCompanyParameter()
        {
            var companiesParameters = await _mediator.Send(new GetCompanyParameterListQuery());
            var companyParameter = companiesParameters.FirstOrDefault();
            return companyParameter;
        }

        private int getProductDoseTypeInterval(string interval, List<ProductDosesViewModel> productDoseslist)
        {
            foreach (var productDose in productDoseslist)
            {
                if (productDose.DoseType.Equals(interval))
                {
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
    }
}