using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class ApplicationAppService : IApplicationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public ApplicationAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
        {
            var applications = await _queryContext.AllApplications.ToListAsync();
            var applicationsViewModel = applications.Select(r => _mapper.Map<ApplicationViewModel>(r)).ToList();
            return applicationsViewModel;
        }

        public async Task<int> GetApplicationNumberByPersonId(Guid personId)
        {
           
            var applicationNumber = (from a in _context.Applications
                                     join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                     where ats.BorrowerPersonId.Equals(personId)
                                     select a.ID).Count();

            return applicationNumber;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAvailableApplicationsByPersonId(Guid personId)
        {
            return null;
        }

        public async Task<IEnumerable<ApplicationHistoryViewModel>> GetHistoryApplicationsByPersonId(Guid personId)
        {

            var availableApplications = (from a in _context.Applications
                                         join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                         join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                                         join b in _context.Budgets on bp.BudgetId equals b.ID
                                         join ps2 in _context.Persons on b.PersonId equals ps2.ID
                                         join p in _context.Products on bp.ProductId equals p.ID
                                         join psb in _context.ProductsSummariesBatches on a.ProductSummaryBatchId equals psb.ID
                                         join u in _context.Users on a.UserId equals u.ID
                                         join ps in _context.Persons on u.PersonId equals ps.ID
                                         where ats.BorrowerPersonId.Equals(personId)
                                         select new
                                         {
                                             ApplicationId = a.ID,
                                             ApplicationDate = a.ApplicationDate,
                                             InclusionDate = a.InclusionDate,
                                             ApplicationPlace = a.ApplicationPlace,
                                             DoseType = a.DoseType,
                                             RouteOfAdministration = a.RouteOfAdministration,
                                             Register = a.Register,
                                             ProductName = p.Name,
                                             Batch = psb.Batch,
                                             ProductSummaryBatchId = psb.ID,
                                             UserPersonName = ps.Name,
                                             BudgetNumber = b.BudgetNumber,
                                             PersonResponsible = ps2.Name
                                         }).ToList();

            List<ApplicationHistoryViewModel> listApplicationHistoryViewModel = new List<ApplicationHistoryViewModel>();

            foreach (var availableApplication in availableApplications)
            {
                ApplicationHistoryViewModel applicationHistoryViewModel = new ApplicationHistoryViewModel();
                applicationHistoryViewModel.ApplicationId = availableApplication.ApplicationId;
                applicationHistoryViewModel.ApplicationDate = availableApplication.ApplicationDate;
                applicationHistoryViewModel.InclusionDate = availableApplication.InclusionDate;
                applicationHistoryViewModel.ApplicationPlace = availableApplication.ApplicationPlace;
                applicationHistoryViewModel.DoseType = availableApplication.DoseType;
                applicationHistoryViewModel.RouteOfAdministration = availableApplication.RouteOfAdministration;
                applicationHistoryViewModel.Register = availableApplication.Register;
                applicationHistoryViewModel.ProductName = availableApplication.ProductName;
                applicationHistoryViewModel.Batch = availableApplication.Batch;
                applicationHistoryViewModel.ProductSummaryBatchId = availableApplication.ProductSummaryBatchId;
                applicationHistoryViewModel.UserPersonName = availableApplication.UserPersonName;
                applicationHistoryViewModel.BudgetNumber = availableApplication.BudgetNumber;
                applicationHistoryViewModel.PersonResponsible = availableApplication.PersonResponsible;

                listApplicationHistoryViewModel.Add(applicationHistoryViewModel);
            }


            return listApplicationHistoryViewModel;
        }

        public ApplicationViewModel GetById(Guid id)
        {
            var application = _mapper.Map<ApplicationViewModel>(_queryContext.AllApplications.Where(r => r.ID == id).First());
            return application;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetByName(string name)
        {
            var applications = await _queryContext.AllApplications.ToListAsync();
            var applicationsViewModel = applications
                .Select(r => _mapper.Map<ApplicationViewModel>(r)).ToList();
            return applicationsViewModel;
        }
    }
}
