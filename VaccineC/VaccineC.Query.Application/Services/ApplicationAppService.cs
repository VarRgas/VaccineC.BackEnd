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
                                           StartTime = e.StartTime,
                                           SipniIntegrationId = a.SipniIntegrationId,
                                           SbimVaccineId = p.SbimVaccinesId
                                       }).OrderByDescending(a => a.Register).ToList();

            foreach (var history in historyApplications) {
                if (history.SbimVaccineId != null && history.SipniIntegrationId == null)
                {
                    history.IntegrationSituation = "error";
                } 
                else if (history.SbimVaccineId != null && history.SipniIntegrationId != null)
                {
                    history.IntegrationSituation = "success";
                }
                else if (history.SbimVaccineId == null) 
                {
                    history.IntegrationSituation = "invalid";
                }
            
            }

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

        public async Task<bool> GetPersonApplicationProductSameDay(Guid personId, Guid productId)
        {

            var minimumHour = new TimeSpan(0, 0, 0);
            var maximumHour = new TimeSpan(23, 59, 59);
            DateTime now = DateTime.Now.Date;

            var applicationId = (from a in _context.Applications
                               join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                               join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                               where ats.BorrowerPersonId.Equals(personId)
                               where bp.ProductId.Equals(productId)
                               where a.ApplicationDate >= now + minimumHour
                               where a.ApplicationDate <= now + maximumHour
                               select a.ID).ToList();

            if (applicationId.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ApplicationPersonGenderViewModel>> GetApplicationsByPersonGender(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            List<ApplicationPersonGenderViewModel> listApplicationPersonGenderViewModel = new List<ApplicationPersonGenderViewModel>();

            var numberApplicationsFem = (from a in _context.Applications
                                   join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                   join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                                   join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                                   where pf.Gender.Equals("F")
                                   where a.ApplicationDate >= dateSearchMinimum.Date
                                   where a.ApplicationDate <= dateSearchMaximum.Date
                                   select a.ID).Count();

            var numberApplicationsMas = (from a in _context.Applications
                                         join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                         join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                                         join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                                         where pf.Gender.Equals("M")
                                         where a.ApplicationDate >= dateSearchMinimum.Date
                                         where a.ApplicationDate <= dateSearchMaximum.Date
                                         select a.ID).Count();

            var numberApplicationsOth = (from a in _context.Applications
                                         join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                         join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                                         join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                                         where pf.Gender.Equals("O")
                                         where a.ApplicationDate >= dateSearchMinimum.Date
                                         where a.ApplicationDate <= dateSearchMaximum.Date
                                         select a.ID).Count();

            var applicationPersonFem = new ApplicationPersonGenderViewModel
            {
                Gender = "Feminino",
                NumberOfApplications = numberApplicationsFem
            };

            var applicationPersonMas = new ApplicationPersonGenderViewModel
            {
                Gender = "Masculino",
                NumberOfApplications = numberApplicationsMas
            };

            var applicationPersonOth = new ApplicationPersonGenderViewModel
            {
                Gender = "Outro",
                NumberOfApplications = numberApplicationsOth
            };

            listApplicationPersonGenderViewModel.Add(applicationPersonFem);
            listApplicationPersonGenderViewModel.Add(applicationPersonMas);
            listApplicationPersonGenderViewModel.Add(applicationPersonOth);

            return listApplicationPersonGenderViewModel;
        }

        public async Task<IEnumerable<ApplicationProductViewModel>> GetApplicationsByProductId(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            List<ApplicationProductViewModel> listApplicationProductViewModel = new List<ApplicationProductViewModel>();

            var productsId = (from a in _context.Applications
                              join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                              where a.ApplicationDate >= dateSearchMinimum.Date
                              where a.ApplicationDate <= dateSearchMaximum.Date
                              select bp.ProductId).Distinct().ToList();

            foreach (var productId in productsId) {
                var numberApplications = (from a in _context.Applications
                                          join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                                          where a.ApplicationDate >= dateSearchMinimum.Date
                                          where a.ApplicationDate <= dateSearchMaximum.Date
                                          where bp.ProductId.Equals(productId)
                                          select a.ID).Count();

                var productName = (from p in _context.Products
                                   where p.ID.Equals(productId)
                                   select p.Name).FirstOrDefault();

                ApplicationProductViewModel applicationProductViewModel = new ApplicationProductViewModel();
                applicationProductViewModel.ProductName = productName;
                applicationProductViewModel.NumberOfApplications = numberApplications;

                listApplicationProductViewModel.Add(applicationProductViewModel);
            }

            return listApplicationProductViewModel;
        }

        public async Task<IEnumerable<ApplicationPersonAgeViewModel>> GetApplicationsByPersonAge(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            List<ApplicationPersonAgeViewModel> listApplicationPersonAgeViewModel = new List<ApplicationPersonAgeViewModel>();

            var personsBirthDates = (from a in _context.Applications
                                      join ats in _context.Authorizations on a.AuthorizationId equals ats.ID
                                      join p in _context.Persons on ats.BorrowerPersonId equals p.ID
                                      where a.ApplicationDate >= dateSearchMinimum.Date
                                      where a.ApplicationDate <= dateSearchMaximum.Date
                                      select p.CommemorativeDate).ToList();

            ApplicationPersonAgeViewModel age0to9 = new ApplicationPersonAgeViewModel();
            age0to9.AgeInterval = "0 a 9 anos";

            ApplicationPersonAgeViewModel age10to19 = new ApplicationPersonAgeViewModel();
            age10to19.AgeInterval = "10 a 19 anos";

            ApplicationPersonAgeViewModel age20to29 = new ApplicationPersonAgeViewModel();
            age20to29.AgeInterval = "20 a 29 anos";

            ApplicationPersonAgeViewModel age30to39 = new ApplicationPersonAgeViewModel();
            age30to39.AgeInterval = "30 a 39 anos";

            ApplicationPersonAgeViewModel age40to49 = new ApplicationPersonAgeViewModel();
            age40to49.AgeInterval = "40 a 49 anos";

            ApplicationPersonAgeViewModel age50to59 = new ApplicationPersonAgeViewModel();
            age50to59.AgeInterval = "50 a 59 anos";

            ApplicationPersonAgeViewModel age60to69 = new ApplicationPersonAgeViewModel();
            age60to69.AgeInterval = "60 a 69 anos";

            ApplicationPersonAgeViewModel age70plus = new ApplicationPersonAgeViewModel();
            age70plus.AgeInterval = "70 anos +";

            foreach (var personBirthDate in personsBirthDates) {

                int personAge = getAge(personBirthDate);

                if (personAge >= 0 && personAge <= 9)
                {
                    age0to9.NumberOfApplications++;
                }
                else if (personAge >= 10 && personAge <= 19)
                {
                    age10to19.NumberOfApplications++;
                }
                else if (personAge >= 20 && personAge <= 29)
                {
                    age20to29.NumberOfApplications++;
                }
                else if (personAge >= 30 && personAge <= 39)
                {
                    age30to39.NumberOfApplications++;
                }
                else if (personAge >= 40 && personAge <= 49)
                {
                    age40to49.NumberOfApplications++;
                }
                else if (personAge >= 50 && personAge <= 59)
                {
                    age50to59.NumberOfApplications++;
                }
                else if (personAge >= 60 && personAge <= 69)
                {
                    age60to69.NumberOfApplications++;
                }
                else if (personAge >= 70)
                {
                    age70plus.NumberOfApplications++;
                }
            }

            listApplicationPersonAgeViewModel.Add(age0to9);
            listApplicationPersonAgeViewModel.Add(age10to19);
            listApplicationPersonAgeViewModel.Add(age20to29);
            listApplicationPersonAgeViewModel.Add(age30to39);
            listApplicationPersonAgeViewModel.Add(age40to49);
            listApplicationPersonAgeViewModel.Add(age50to59);
            listApplicationPersonAgeViewModel.Add(age60to69);
            listApplicationPersonAgeViewModel.Add(age70plus);
            return listApplicationPersonAgeViewModel;
        }

        public async Task<IEnumerable<ApplicationSipniIntegrationViewModel>> GetSipniIntegrationSituation(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            List<ApplicationSipniIntegrationViewModel> listApplicationSipniIntegrationViewModel = new List<ApplicationSipniIntegrationViewModel>();

            var applications = (from a in _context.Applications
                                where a.ApplicationDate >= dateSearchMinimum.Date
                                where a.ApplicationDate <= dateSearchMaximum.Date
                                select a).ToList();

            ApplicationSipniIntegrationViewModel success = new ApplicationSipniIntegrationViewModel();
            success.Situation = "Comunicado";

            ApplicationSipniIntegrationViewModel error = new ApplicationSipniIntegrationViewModel();
            error.Situation = "Não Comunicado";

            foreach (var application in applications) {
                if (application.SipniIntegrationId == null) 
                {
                    error.NumberOfIntegrations++;
                }
                else
                {
                    success.NumberOfIntegrations++;
                }
            }

            listApplicationSipniIntegrationViewModel.Add(success);
            listApplicationSipniIntegrationViewModel.Add(error);

            return listApplicationSipniIntegrationViewModel;
        }

        private int getAge(DateTime? date)
        {
            DateTime personBirthDate = (DateTime)date;
            int age = 0;
            age = DateTime.Now.Subtract(personBirthDate).Days;
            age = age / 365;
            return age;
        }

        public async Task<ApplicationNumberViewModel> GetApplicationsNumbers(int month, int year)
        {
            DateTime dateSearchMinimum = new DateTime(year, month, 1);
            DateTime dateSearchMaximum = new DateTime(year, month, DateTime.DaysInMonth(year, month));
           ApplicationNumberViewModel applicationNumberViewModel = new ApplicationNumberViewModel();

            var applicationsCompleted = (from a in _context.Applications
                                         where a.ApplicationDate >= dateSearchMinimum.Date
                                         where a.ApplicationDate <= dateSearchMaximum.Date
                                         select a.ID).Count();


            var applicationsPending = (from a in _context.Authorizations
                                  join ap in _context.Applications on a.ID equals ap.AuthorizationId into _ap
                                  from x in _ap.DefaultIfEmpty()
                                  join e in _context.Events on a.EventId equals e.ID
                                  join bp in _context.BudgetsProducts on a.BudgetProductId equals bp.ID
                                  join p in _context.Products on bp.ProductId equals p.ID
                                  join b in _context.Budgets on bp.BudgetId equals b.ID
                                  join prb in _context.Persons on b.PersonId equals prb.ID
                                  where a.Situation.Equals("C")
                                  where x.ID.Equals(null)
                                  where e.StartDate >= dateSearchMinimum.Date
                                  where e.StartDate <= dateSearchMaximum.Date
                                  select a.ID).Count();

            applicationNumberViewModel.ApplicationsCompleted = applicationsCompleted;
            applicationNumberViewModel.ApplicationsPending = applicationsPending;

            return applicationNumberViewModel;
        }
    } 
}
