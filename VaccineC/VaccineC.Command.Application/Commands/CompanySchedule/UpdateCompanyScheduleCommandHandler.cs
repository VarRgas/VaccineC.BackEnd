using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class UpdateCompanyScheduleCommandHandler : IRequestHandler<UpdateCompanyScheduleCommand, IEnumerable<CompanyScheduleViewModel>>
    {
        private readonly ICompanyScheduleRepository _companyScheduleRepository;
        private readonly ICompanyScheduleAppService _companyScheduleAppService;


        public UpdateCompanyScheduleCommandHandler(ICompanyScheduleRepository companyScheduleRepository, ICompanyScheduleAppService companyScheduleAppService)
        {
            _companyScheduleRepository = companyScheduleRepository;
            _companyScheduleAppService = companyScheduleAppService;
        }

        public async Task<IEnumerable<CompanyScheduleViewModel>> Handle(UpdateCompanyScheduleCommand request, CancellationToken cancellationToken)
        {

            var companySchedule = _companyScheduleRepository.GetById(request.ID);

            if (companySchedule == null)
            {
                throw new ArgumentException("Horário não encontrado!");
            }

            companySchedule.SetDay(request.Day);
            companySchedule.SetStartTime(request.StartTime);
            companySchedule.SetFinalTime(request.FinalTime);
            companySchedule.SetRegister(DateTime.Now);

            await _companyScheduleRepository.SaveChangesAsync();

            return await _companyScheduleAppService.GetAllCompaniesSchedulesByCompanyID(companySchedule.CompanyId);
        }
    }
}
