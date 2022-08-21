using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceByIdQueryHandler : IRequestHandler<GetResourceByIdQuery, ResourceViewModel>
    {

        private readonly IMediator _mediator;

        public GetResourceByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResourceViewModel> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var resources = await _mediator.Send(new GetResourceListQuery());
            var resource = resources.FirstOrDefault(pf => pf.ID == request.Id);
            return resource;

        }
    }
}
