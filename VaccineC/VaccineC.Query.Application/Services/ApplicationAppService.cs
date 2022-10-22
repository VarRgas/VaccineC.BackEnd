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

        public async Task<IEnumerable<ApplicationAvailableViewModel>> GetAvailableApplicationsByPersonId(Guid personId)
        {

            var dateNow = DateTime.Now;
            var maximumHour = new TimeSpan(23, 59, 59);

            var authorizations = (from a in _context.Authorizations
                                    join ap in _context.Applications on a.ID equals ap.AuthorizationId into _ap
                                    from x in _ap.DefaultIfEmpty()
                                    join e in _context.Events on a.EventId equals e.ID
                                    join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                                    join p in _context.Products on bp.ProductId equals p.ID
                                    join b in _context.Budgets on bp.BudgetId equals b.ID
                                    join prb in _context.Persons on b.PersonId equals prb.ID
                                    where a.Situation.Equals("C")
                                    where x.ID.Equals(null)
                                    where a.BorrowerPersonId.Equals(personId)
                                    where e.StartDate <= dateNow.Date + maximumHour
                                    orderby e.StartDate, e.StartTime
                                    select new ApplicationAvailableViewModel
                                    {
                                        ProductId = p.ID,
                                        ProductName = p.Name,
                                        DoseType = bp.ProductDose,
                                        AuthorizationId = a.ID,
                                        AuthorizationNumber = a.AuthorizationNumber,
                                        TypeOfService = a.TypeOfService,
                                        StartDate = e.StartDate,
                                        StartTime = e.StartTime,
                                        BudgetId = b.ID,
                                        BudgetNumber = b.BudgetNumber,
                                        PersonBudget = prb.Name,
                                        TotalBudgetAmount = b.TotalBudgetAmount
                                    }).ToList();

            return authorizations;
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
                                         select new ApplicationHistoryViewModel
                                         {
                                             ApplicationId = a.ID,
                                             ApplicationDate = a.ApplicationDate,
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
                                         }).OrderByDescending(a => a.Register).ToList();

            return availableApplications;
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
