using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class AddCompanyScheduleOnDemandCommandHandler : IRequestHandler<AddCompanyScheduleOnDemandCommand, IEnumerable<CompanyScheduleViewModel>>
    {

        private readonly ICompanyScheduleRepository _companyScheduleRepository;
        private readonly ICompanyScheduleAppService _companyScheduleAppService;
        private readonly VaccineCCommandContext _ctx;

        public AddCompanyScheduleOnDemandCommandHandler(ICompanyScheduleRepository companyScheduleRepository, ICompanyScheduleAppService companyScheduleAppService, VaccineCCommandContext ctx)
        {
            _companyScheduleRepository = companyScheduleRepository;
            _companyScheduleAppService = companyScheduleAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<CompanyScheduleViewModel>> Handle(AddCompanyScheduleOnDemandCommand request, CancellationToken cancellationToken)
        {
            Guid companyId = Guid.NewGuid();

            List<CompanyScheduleViewModel> listCompanyScheduleViewModel = request.ListCompanyScheduleViewModel;
            
            foreach (CompanyScheduleViewModel companyScheduleViewModel in listCompanyScheduleViewModel)
            {

                Domain.Entities.CompanySchedule newCompanySchedule = new Domain.Entities.CompanySchedule(
                    Guid.NewGuid(),
                    companyScheduleViewModel.CompanyId,
                    companyScheduleViewModel.Day,
                    companyScheduleViewModel.StartTime,
                    companyScheduleViewModel.FinalTime,
                    DateTime.Now
                    );

                _companyScheduleRepository.Add(newCompanySchedule);
                companyId = newCompanySchedule.CompanyId;

                await _companyScheduleRepository.SaveChangesAsync();
            }
            return await _companyScheduleAppService.GetAllCompaniesSchedulesByCompanyID(companyId);

        }
    }
}
