using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyConfigForAuthorizationQueryHandler : IRequestHandler<GetCompanyConfigForAuthorizationQuery, AuthorizationParameterViewModel>
    {

        private readonly ICompanyAppService _companyAppService;

        public GetCompanyConfigForAuthorizationQueryHandler(ICompanyAppService companyAppService, IMediator mediator)
        {
            _companyAppService = companyAppService;
        }

        public async Task<AuthorizationParameterViewModel> Handle(GetCompanyConfigForAuthorizationQuery request, CancellationToken cancellationToken)
        {

            CompanyViewModel companyViewModel = _companyAppService.GetFirst();

            if (companyViewModel == null)
            {
                throw new ArgumentException("Empresa não encontrada!");
            }

            var companyParameterViewModel = _companyAppService.GetCompanyParameterByCompanyId(companyViewModel.ID);
           
            if (companyParameterViewModel == null)
            {
                throw new ArgumentException("Parâmetros não encontrados!");
            }


            var listTime = _companyAppService.GetMinMaxCompanySchedule(companyViewModel.ID);

            if (listTime.Count() <= 0)
            {
                throw new ArgumentException("Horários não encontrados!");
            }

            return new AuthorizationParameterViewModel()
            {
                ApplicationTimePerMinute = companyParameterViewModel.ApplicationTimePerMinute,
                ScheduleColor = companyParameterViewModel.ScheduleColor,
                MinTime = listTime[0],
                MaxTime = listTime[1]

            };

        }
    }
}
