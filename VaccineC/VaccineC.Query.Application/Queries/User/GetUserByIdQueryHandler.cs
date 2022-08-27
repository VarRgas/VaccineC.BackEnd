using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {

        private readonly IMediator _mediator;

        public GetUserByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetUserListQuery());
            var user = users.FirstOrDefault(u => u.ID == request.Id);
            return user;
        }
    }
}
