using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompanyScheduleByIdQueryHandler : IRequestHandler<GetCompanyScheduleByIdQuery, CompanyScheduleViewModel>
    {
        private readonly IMediator _mediator;

        public GetCompanyScheduleByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CompanyScheduleViewModel> Handle(GetCompanyScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var companiesSchedules = await _mediator.Send(new GetCompanyScheduleListQuery());
            var companySchedule = companiesSchedules.FirstOrDefault(pf => pf.ID == request.Id);
            return companySchedule;
        }
    }
}
