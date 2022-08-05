using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Example
{
    public class GetExampleByIdQueryHandler : IRequestHandler<GetExampleByIdQuery, ExampleViewModel>
    {
        private readonly IMediator _mediator;

        public GetExampleByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ExampleViewModel> Handle(GetExampleByIdQuery request, CancellationToken cancellationToken)
        {
            var guests = await _mediator.Send(new GetExampleListQuery());
            var guest = guests.FirstOrDefault(r => r.Id == request.Id);
            return guest;
        }
    }
}
