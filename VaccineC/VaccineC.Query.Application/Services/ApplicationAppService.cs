using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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

            var historyApplications = (from a in _context.Applications
                                       join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                       join e in _context.Events on ats.EventId equals e.ID
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
                                           PersonResponsible = ps2.Name,
                                           StartDate = e.StartDate,
                                           StartTime = e.StartTime
                                       }).OrderByDescending(a => a.Register).ToList();

            return historyApplications;
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

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(string personName, DateTime applicationDate, Guid responsibleId)
        {

            DateTime dt = new DateTime(1901, 1, 1);

            if (!personName.Equals("emptyName") && !responsibleId.Equals(Guid.Empty) && applicationDate.Date > dt)
            {
                return await GetAllApplicationsByAllParameter(personName, applicationDate.Date, responsibleId);
            }
            else if (personName.Equals("emptyName") && !responsibleId.Equals(Guid.Empty) && applicationDate.Date > dt)
            {
                return await GetAllApplicationsByParameter(applicationDate.Date, responsibleId);
            } 
            else if (personName.Equals("emptyName") && responsibleId.Equals(Guid.Empty) && applicationDate.Date > dt)
            {
                return await GetAllApplicationsByParameter(applicationDate.Date);
            }
            else if (personName.Equals("emptyName") && !responsibleId.Equals(Guid.Empty) && applicationDate.Date == dt)
            {
                return await GetAllApplicationsByParameter(responsibleId);
            }
            else if (!personName.Equals("emptyName") && responsibleId.Equals(Guid.Empty) && applicationDate.Date > dt) {
                return await GetAllApplicationsByParameter(personName, applicationDate.Date);
            }
            else if (!personName.Equals("emptyName") && !responsibleId.Equals(Guid.Empty) && applicationDate.Date == dt) {
                return await GetAllApplicationsByParameter(personName, responsibleId);
            }else
            {
                return null;
            }
        }

        private async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByAllParameter(string personName, DateTime applicationDate, Guid responsibleId)
        {
            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);

            var personsId = (from a in _context.Applications
                             join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                             join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                             join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                             join b in _context.Budgets on bp.BudgetId equals b.ID
                             where p.Name.ToLower().Contains(personName.ToLower())
                             where b.PersonId.Equals(responsibleId)
                             where a.Register >= applicationDate.Date + minimumHour
                             where a.Register <= applicationDate.Date + maximumHour
                             select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons
                           join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                           where personsId.Contains(p.ID)
                           select new PersonViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProfilePic = p.ProfilePic,
                               CommemorativeDate = p.CommemorativeDate,
                               PersonsPhysical = new PersonsPhysicalViewModel
                               {
                                   Gender = pf.Gender
                               }

                           }).OrderBy(p => p.Name).ToList();

            return persons;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(DateTime applicationDate, Guid responsibleId)
        {
            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);

            var personsId = (from a in _context.Applications
                             join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                             join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                             join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                             join b in _context.Budgets on bp.BudgetId equals b.ID
                             where b.PersonId.Equals(responsibleId)
                             where a.Register >= applicationDate.Date + minimumHour
                             where a.Register <= applicationDate.Date + maximumHour
                             select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons
                           join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                           where personsId.Contains(p.ID)
                           select new PersonViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProfilePic = p.ProfilePic,
                               CommemorativeDate = p.CommemorativeDate,
                               PersonsPhysical = new PersonsPhysicalViewModel
                               {
                                   Gender = pf.Gender
                               }

                           }).OrderBy(p => p.Name).ToList();

            return persons;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(DateTime applicationDate)
        {
            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);

            var personsId = (from a in _context.Applications
                              join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                              join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                              where a.Register >= applicationDate.Date + minimumHour
                              where a.Register <= applicationDate.Date + maximumHour
                              select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons 
                             join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                             where personsId.Contains(p.ID)
                             select new PersonViewModel
                                 {
                                       ID = p.ID,
                                       Name = p.Name,
                                       ProfilePic = p.ProfilePic,
                                       CommemorativeDate = p.CommemorativeDate,
                                       PersonsPhysical = new PersonsPhysicalViewModel
                                       {
                                           Gender = pf.Gender
                                       }

                             }).OrderBy(p => p.Name).ToList();

            return persons;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(Guid responsibleId)
        {
            var personsId = (from a in _context.Applications
                             join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                             join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                             join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                             join b in _context.Budgets on bp.BudgetId equals b.ID
                             where b.PersonId.Equals(responsibleId)
                             select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons
                           join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                           where personsId.Contains(p.ID)
                           select new PersonViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProfilePic = p.ProfilePic,
                               CommemorativeDate = p.CommemorativeDate,
                               PersonsPhysical = new PersonsPhysicalViewModel
                               {
                                   Gender = pf.Gender
                               }

                           }).OrderBy(p => p.Name).ToList();

            return persons;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(string personName, DateTime applicationDate)
        {
            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);

            var personsId = (from a in _context.Applications
                             join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                             join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                             where a.Register >= applicationDate.Date + minimumHour
                             where a.Register <= applicationDate.Date + maximumHour
                             where p.Name.ToLower().Contains(personName.ToLower())
                             select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons
                           join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                           where personsId.Contains(p.ID)
                           select new PersonViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProfilePic = p.ProfilePic,
                               CommemorativeDate = p.CommemorativeDate,
                               PersonsPhysical = new PersonsPhysicalViewModel
                               {
                                   Gender = pf.Gender
                               }

                           }).OrderBy(p => p.Name).ToList();

            return persons;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllApplicationsByParameter(string personName, Guid responsibleId)
        {
            var personsId = (from a in _context.Applications
                             join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                             join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                             join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                             join b in _context.Budgets on bp.BudgetId equals b.ID
                             where b.PersonId.Equals(responsibleId)
                             where p.Name.ToLower().Contains(personName.ToLower())
                             select p.ID).Distinct().ToList();

            var persons = (from p in _context.Persons
                           join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                           where personsId.Contains(p.ID)
                           select new PersonViewModel
                           {
                               ID = p.ID,
                               Name = p.Name,
                               ProfilePic = p.ProfilePic,
                               CommemorativeDate = p.CommemorativeDate,
                               PersonsPhysical = new PersonsPhysicalViewModel
                               {
                                   Gender = pf.Gender
                               }

                           }).OrderBy(p => p.Name).ToList();

            return persons;
        }
    } 
}
