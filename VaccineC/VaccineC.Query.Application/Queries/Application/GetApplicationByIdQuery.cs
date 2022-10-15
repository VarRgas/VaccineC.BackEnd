using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationByIdQuery : IRequest<ApplicationViewModel>
    {
        public Guid Id;

        public GetApplicationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
