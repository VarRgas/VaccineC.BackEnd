using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class AuthorizationAppService : IAuthorizationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public AuthorizationAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllAsync()
        {
            var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
            var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).ToList();
            return authorizationsViewModel;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllByAuthNumber(int authNumber, string situation, Guid responsibleId)
        {
            if (situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (!situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (situation.Equals("T") && responsibleId != Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.Situation.Equals(situation) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllByBorrowerName(string borrowerName, string situation, Guid responsibleId)
        {

            if (borrowerName.Count() < 3)
            {
                throw new ArgumentException("É necessário informar no mínimo 3 caracteres para realizar a busca!");
            }

            if (situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower())).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if(!situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (situation.Equals("T") && responsibleId != Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId) && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }

        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllForApplication()
        {

            var dateNow = DateTime.Now;
            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);

            var authorizationsId = (from a in _context.Authorizations
                                                  join ap in _context.Applications on a.ID equals ap.AuthorizationId into _ap
                                                  from x in _ap.DefaultIfEmpty()
                                                  join e in _context.Events on a.EventId equals e.ID
                                                  where a.Situation.Equals("C")
                                                  where x.ID.Equals(null)
                                                  where e.StartDate >= dateNow.Date + minimumHour
                                                  where e.StartDate <= dateNow.Date + maximumHour
                                                  select a.ID).ToList();

            var allAuthorizations = await _queryContext.AllAuthorizations.ToListAsync();
            var authorizationsViewModel = allAuthorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).ToList();

            List<AuthorizationViewModel> listAuthorizationViewModelReturn = new List<AuthorizationViewModel>();

            foreach (var authorization in authorizationsViewModel)
            {
                foreach (var authorizationId in authorizationsId) {
                    if (authorization.ID.Equals(authorizationId)) {
                        listAuthorizationViewModelReturn.Add(authorization);
                    }
                }
            }

            return listAuthorizationViewModelReturn;
        }

        public async Task<AuthorizationDashInfoViewModel> GetAuthorizationDashInfo(int month, int year)
        {

            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            AuthorizationDashInfoViewModel authorizationDashInfoViewModel = new AuthorizationDashInfoViewModel();

            var authorizations = (from a in _context.Authorizations
                                  join e in _context.Events on a.EventId equals e.ID
                                  where e.StartDate >= dateSearchMinimum.Date
                                  where e.StartDate <= dateSearchMaximum.Date
                                  select a).ToList();

            foreach(var auth in authorizations)
            {
                if (auth.Situation.Equals("P")) 
                {
                    authorizationDashInfoViewModel.AuthorizationConcludedNumber++;
                }
                else if (auth.Situation.Equals("X"))
                {
                    authorizationDashInfoViewModel.AuthorizationCanceledNumber++;
                }
                else
                {
                    authorizationDashInfoViewModel.AuthorizationScheduleNumber++;
                }

                if (auth.Notify.Equals("N")) 
                {
                    authorizationDashInfoViewModel.AuthorizationsWithoutNotification++;
                }
                else
                {
                    authorizationDashInfoViewModel.AuthorizationsWithNotification++;
                }
            }

            var authorizationsNotificationsIds = (from an in _context.AuthorizationsNotifications
                                                  join ats in _context.Authorizations on an.AuthorizationId equals ats.ID
                                                  join e in _context.Events on ats.EventId equals e.ID
                                                  where e.StartDate >= dateSearchMinimum.Date
                                                  where e.StartDate <= dateSearchMaximum.Date
                                                  select an.ReturnId).ToList();

            authorizationDashInfoViewModel.authorizationNotificationDashInfos = await getSmsSituation(authorizationsNotificationsIds);

            return authorizationDashInfoViewModel;
        }

        private async Task<List<AuthorizationNotificationDashInfo>> getSmsSituation(List<String> authorizationsNotificationsIds)
        {
            string url = "https://api.smsdev.com.br/v1/dlr";
            string key = "M30A09QH6Z80WHY0DFS9QECUBIBUVBVT67P50CY9BYSL54W6A504FO9XLB5VLLAD7Y6WUW9PELVVI90LNCYA05RSJU0LY9MIXYIZ06VOQVZXXAJ9N45LQ25QS7IS5V7B";

            List<AuthorizationNotificationDashInfo> listAuthorizationNotificationDashInfo = new List<AuthorizationNotificationDashInfo>();
            
            AuthorizationNotificationDashInfo recebida = new AuthorizationNotificationDashInfo();
            recebida.Description = "RECEBIDA";

            AuthorizationNotificationDashInfo enviada = new AuthorizationNotificationDashInfo();
            enviada.Description = "ENVIADA";

            AuthorizationNotificationDashInfo erro = new AuthorizationNotificationDashInfo();
            erro.Description = "ERRO";

            AuthorizationNotificationDashInfo fila = new AuthorizationNotificationDashInfo();
            fila.Description = "FILA";

            AuthorizationNotificationDashInfo cancelada = new AuthorizationNotificationDashInfo();
            cancelada.Description = "CANCELADA";

            AuthorizationNotificationDashInfo blacklist = new AuthorizationNotificationDashInfo();
            blacklist.Description = "BLACKLIST";

            foreach (var id in authorizationsNotificationsIds) 
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(url);
                    cliente.DefaultRequestHeaders.Accept.Clear();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new
                    {
                        key = key,
                        id = id
                    };

                    var response = await cliente.PostAsJsonAsync("", data);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseString = await response.Content.ReadAsStringAsync();
                        var situation = JObject.Parse(responseString)["descricao"].ToString();
                      
                        if (situation.ToUpper().Trim().Equals("RECEBIDA"))
                        {
                            recebida.Quantity++;
                        }
                        else if (situation.ToUpper().Trim().Equals("ENVIADA"))
                        {
                            enviada.Quantity++;
                        }
                        else if (situation.ToUpper().Trim().Equals("ERRO"))
                        {
                            erro.Quantity++;
                        }
                        else if (situation.ToUpper().Trim().Equals("FILA"))
                        {
                            fila.Quantity++;
                        }
                        else if (situation.ToUpper().Trim().Equals("CANCELADA"))
                        {
                            cancelada.Quantity++;
                        }
                        else if (situation.ToUpper().Trim().Equals("BLACKLIST"))
                        {
                            blacklist.Quantity++;
                        }

                    }
                }
            }

            listAuthorizationNotificationDashInfo.Add(recebida);
            listAuthorizationNotificationDashInfo.Add(enviada);
            listAuthorizationNotificationDashInfo.Add(erro);
            listAuthorizationNotificationDashInfo.Add(fila);
            listAuthorizationNotificationDashInfo.Add(cancelada);
            listAuthorizationNotificationDashInfo.Add(blacklist);

            return listAuthorizationNotificationDashInfo;
        }

        public AuthorizationViewModel GetById(Guid id)
        {
            var authorization = _mapper.Map<AuthorizationViewModel>(_queryContext.AllAuthorizations.Where(r => r.ID == id).First());
            return authorization;
        }

        public async Task<IEnumerable<AuthorizationSummarySituationViewModel>> GetSummarySituationAuthorization()
        {

            List<AuthorizationSummarySituationViewModel> listAuthorizationSummarySituationViewModel = new List<AuthorizationSummarySituationViewModel>();

            var products = await _queryContext.AllProducts.ToListAsync();
            var productsViewModel = products.Select(r => _mapper.Map<ProductViewModel>(r)).ToList();

            DateTime now = DateTime.Today;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            foreach (var product in productsViewModel)
            {

                AuthorizationSummarySituationViewModel authorizationSummarySituationViewModel = new AuthorizationSummarySituationViewModel();

                var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
                var totalUnitsProduct = productsSummariesBatches
                    .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                    .Where(r => r.ProductsId == product.ID)
                    .Sum(r => r.NumberOfUnitsBatch);

                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var totalAuthorizationsByProduct = authorizations
                    .Select(r => _mapper.Map<AuthorizationViewModel>(r))
                        .Where(r => r.BudgetProduct.ProductId == product.ID && r.Event.StartDate >= firstDay && r.Event.StartDate <= lastDay && r.Situation.Equals("C")).Count();

                authorizationSummarySituationViewModel.ProductId = product.ID;
                authorizationSummarySituationViewModel.ProductName = product.Name;
                authorizationSummarySituationViewModel.TotalUnitsProduct = (int)totalUnitsProduct;
                authorizationSummarySituationViewModel.TotalAuthorizationsMonth = totalAuthorizationsByProduct;
                authorizationSummarySituationViewModel.TotalUnitsAfterApplication = (int)totalUnitsProduct - totalAuthorizationsByProduct;

                if (totalAuthorizationsByProduct != 0) {
                    listAuthorizationSummarySituationViewModel.Add(authorizationSummarySituationViewModel);
                }
            }

            return listAuthorizationSummarySituationViewModel;
        }

    }
}
