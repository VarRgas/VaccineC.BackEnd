using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Example
{
    public class GetExampleListQueryHandler : IRequestHandler<GetExampleListQuery, IEnumerable<ExampleViewModel>>
    {
        private readonly IExampleAppService _exampleAppService;

        public GetExampleListQueryHandler(IExampleAppService exampleAppService)
        {
            _exampleAppService = exampleAppService;
        }

        public async Task<IEnumerable<ExampleViewModel>> Handle(GetExampleListQuery request, CancellationToken cancellationToken)
        {
            return await _exampleAppService.GetAllAsync();
        }
    }
}
