using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Example
{
    public class GetExampleByIdQuery : IRequest<ExampleViewModel>
    {
        public int Id;

        public GetExampleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
