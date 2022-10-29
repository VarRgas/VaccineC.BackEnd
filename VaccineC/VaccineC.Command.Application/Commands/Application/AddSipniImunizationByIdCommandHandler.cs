using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Application
{
    public class AddSipniImunizationByIdCommandHandler : IRequestHandler<AddSipniImunizationByIdCommand, IEnumerable<ApplicationHistoryViewModel>>
    {
        private readonly IApplicationRepository _repository;
        private readonly IApplicationAppService _appService;
        private readonly VaccineCContext _context;
        private readonly IMediator _mediator;

        public AddSipniImunizationByIdCommandHandler(IApplicationRepository repository, VaccineCContext context, IMediator mediator, IApplicationAppService appService)
        {
            _repository = repository;
            _context = context;
            _mediator = mediator;
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationHistoryViewModel>> Handle(AddSipniImunizationByIdCommand request, CancellationToken cancellationToken)
        {

            var application = _repository.GetById(request.ApplicationId);

            if (application == null) {
                throw new ArgumentException("Aplicação não encontrada!");
            }

            await _mediator.Send(new AddSipniImunizationCommand(application));

            return await _appService.GetHistoryApplicationsByPersonId(request.PersonId);
        }
    }
}
