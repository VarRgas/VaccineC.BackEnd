using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class DeleteCompanyScheduleCommandHandler : IRequestHandler<DeleteCompanyScheduleCommand, IEnumerable<CompanyScheduleViewModel>>
    {
        private readonly ICompanyScheduleRepository _companyScheduleRepository;
        private readonly ICompanyScheduleAppService _companyScheduleAppService;
        private readonly VaccineCCommandContext _ctx;

        public DeleteCompanyScheduleCommandHandler(ICompanyScheduleRepository companyScheduleRepository, ICompanyScheduleAppService companyScheduleAppService, VaccineCCommandContext ctx)
        {
            _companyScheduleRepository = companyScheduleRepository;
            _companyScheduleAppService = companyScheduleAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<CompanyScheduleViewModel>> Handle(DeleteCompanyScheduleCommand request, CancellationToken cancellationToken)
        {
            var companySchedule = _companyScheduleRepository.GetById(request.Id);

            if (companySchedule == null)
            {
                throw new ArgumentException("Horário não encontrado!");
            }

            _companyScheduleRepository.Remove(companySchedule);
            await _companyScheduleRepository.SaveChangesAsync();

            return await _companyScheduleAppService.GetAllCompaniesSchedulesByCompanyID(companySchedule.CompanyId);
        }
    }
}
