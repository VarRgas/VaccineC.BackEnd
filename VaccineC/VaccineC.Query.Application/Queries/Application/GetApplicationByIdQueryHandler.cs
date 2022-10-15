using AutoMapper;
using MediatR;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationViewModel>
    {
        private readonly IMediator _mediator;

        public GetApplicationByIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
        }

        public async Task<ApplicationViewModel> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var applications = await _mediator.Send(new GetApplicationListQuery());
            var application = applications.FirstOrDefault(a => a.ID == request.Id);
            return application;
        }
    }
}
