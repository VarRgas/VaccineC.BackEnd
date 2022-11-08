using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class UserResourceAppService : IUserResourceAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public UserResourceAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<UserResourceViewModel>> GetAllAsync()
        {
            var usersResources = await _queryContext.AllUserResources.ToListAsync();
            var usersResourcesViewModel = usersResources.Select(u => _mapper.Map<UserResourceViewModel>(u)).ToList();
            return usersResourcesViewModel;
        }

        public async Task<IEnumerable<UserResourceViewModel>> GetAllByUser(Guid userId)
        {
            var usersResources = await _queryContext.AllUserResources.ToListAsync();
            var usersResourcesViewModel = usersResources
                .Select(ur => _mapper.Map<UserResourceViewModel>(ur))
                .Where(ur => ur.UsersId.Equals(userId))
                .ToList();
            return usersResourcesViewModel;
        }

        public UserResourceViewModel GetById(Guid id)
        {
            var userResource = _mapper.Map<UserResourceViewModel>(_queryContext.AllUserResources.Where(ur => ur.ID == id).First());
            return userResource;
        }

        public UserResourceViewModel GetByUserResource(Guid usersId, Guid resourcesId)
        {
            var userResource = _mapper.Map<UserResourceViewModel>(_queryContext.AllUserResources.Where(ur => ur.UsersId == usersId && ur.ResourcesId == resourcesId).First());
            return userResource;
        }

        public async Task<UserResourceMenuViewModel> GetUserResourceMenyByUser(Guid userId)
        {

            var userResources = (from ur in _context.UsersResources
                                 join r in _context.Resources on ur.ResourcesId equals r.ID
                                 where ur.UsersId.Equals(userId)
                                 select r).ToList();

            UserResourceMenuViewModel userResourceMenuViewModel = new UserResourceMenuViewModel();
            List<UserResourceMenuRegistrationViewModel> listRegistration = new List<UserResourceMenuRegistrationViewModel>();
            List<UserResourceMenuOperationalViewModel> listOperational = new List<UserResourceMenuOperationalViewModel>();
            List<UserResourceMenuInventoryViewModel> listInventory = new List<UserResourceMenuInventoryViewModel>();
            List<UserResourceMenuManagementViewModel> listManagement = new List<UserResourceMenuManagementViewModel>();

            foreach (var userResource in userResources)
            {
                if (userResource.UrlName.ToLower().Contains("/pessoas".ToLower()))
                {
                    UserResourceMenuRegistrationViewModel userResourcePerson = new UserResourceMenuRegistrationViewModel();
                    userResourcePerson.ResourceName = userResource.Name;
                    userResourcePerson.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listRegistration.Add(userResourcePerson);
                }

                if (userResource.UrlName.ToLower().Contains("/produto".ToLower()))
                {
                    UserResourceMenuRegistrationViewModel userResourceProduct = new UserResourceMenuRegistrationViewModel();
                    userResourceProduct.ResourceName = userResource.Name;
                    userResourceProduct.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listRegistration.Add(userResourceProduct);
                }

                if (userResource.UrlName.ToLower().Contains("/formas-pagamento".ToLower()))
                {
                    UserResourceMenuRegistrationViewModel userResourcePaymentForm = new UserResourceMenuRegistrationViewModel();
                    userResourcePaymentForm.ResourceName = userResource.Name;
                    userResourcePaymentForm.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listRegistration.Add(userResourcePaymentForm);
                }

                if (userResource.UrlName.ToLower().Contains("/agendamento".ToLower()))
                {
                    UserResourceMenuOperationalViewModel userResourceScheduling = new UserResourceMenuOperationalViewModel();
                    userResourceScheduling.ResourceName = userResource.Name;
                    userResourceScheduling.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listOperational.Add(userResourceScheduling);
                }

                if (userResource.UrlName.ToLower().Contains("/aplicacao".ToLower()))
                {
                    UserResourceMenuOperationalViewModel userResourceApplication = new UserResourceMenuOperationalViewModel();
                    userResourceApplication.ResourceName = userResource.Name;
                    userResourceApplication.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listOperational.Add(userResourceApplication);
                }

                if (userResource.UrlName.ToLower().Contains("/orcamentos".ToLower()))
                {
                    UserResourceMenuOperationalViewModel userResourceBudget = new UserResourceMenuOperationalViewModel();
                    userResourceBudget.ResourceName = userResource.Name;
                    userResourceBudget.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listOperational.Add(userResourceBudget);
                }

                if (userResource.UrlName.ToLower().Contains("/movimentar-estoque".ToLower()))
                {
                    UserResourceMenuInventoryViewModel userResourceBatchMovement = new UserResourceMenuInventoryViewModel();
                    userResourceBatchMovement.ResourceName = userResource.Name;
                    userResourceBatchMovement.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listInventory.Add(userResourceBatchMovement);
                }

                if (userResource.UrlName.ToLower().Contains("/situacao-estoque".ToLower()))
                {
                    UserResourceMenuInventoryViewModel userResourceBatchSituation = new UserResourceMenuInventoryViewModel();
                    userResourceBatchSituation.ResourceName = userResource.Name;
                    userResourceBatchSituation.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listInventory.Add(userResourceBatchSituation);
                }

                if (userResource.UrlName.ToLower().Contains("/empresas".ToLower()))
                {
                    UserResourceMenuManagementViewModel userResourceCompany = new UserResourceMenuManagementViewModel();
                    userResourceCompany.ResourceName = userResource.Name;
                    userResourceCompany.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listManagement.Add(userResourceCompany);
                }

                if (userResource.UrlName.ToLower().Contains("/gerenciar-usuarios".ToLower()))
                {
                    UserResourceMenuManagementViewModel userResourceManagementUser = new UserResourceMenuManagementViewModel();
                    userResourceManagementUser.ResourceName = userResource.Name;
                    userResourceManagementUser.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listManagement.Add(userResourceManagementUser);
                }

                if (userResource.UrlName.ToLower().Contains("/visao-faturamento".ToLower()))
                {
                    UserResourceMenuManagementViewModel userResourceDash = new UserResourceMenuManagementViewModel();
                    userResourceDash.ResourceName = userResource.Name;
                    userResourceDash.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listManagement.Add(userResourceDash);
                }

                if (userResource.UrlName.ToLower().Contains("/recursos".ToLower()))
                {
                    UserResourceMenuManagementViewModel userResourceResource = new UserResourceMenuManagementViewModel();
                    userResourceResource.ResourceName = userResource.Name;
                    userResourceResource.ResourceUrl = userResource.UrlName.Split("/")[1];

                    listManagement.Add(userResourceResource);
                }
            }

            userResourceMenuViewModel.listRegistration = listRegistration;
            userResourceMenuViewModel.listOperational = listOperational;
            userResourceMenuViewModel.listInventory = listInventory;
            userResourceMenuViewModel.listManagement = listManagement;

            return userResourceMenuViewModel;
        }
    }
}
