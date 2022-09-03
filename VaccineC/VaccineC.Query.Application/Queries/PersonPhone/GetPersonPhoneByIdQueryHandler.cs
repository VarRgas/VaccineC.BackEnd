using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneByIdQueryHandler : IRequestHandler<GetPersonPhoneByIdQuery, PersonPhoneViewModel>
    {
        private readonly IMediator _mediator;

        public GetPersonPhoneByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonPhoneViewModel> Handle(GetPersonPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            var personPhones = await _mediator.Send(new GetPersonPhoneListQuery());
            var personPhone = personPhones.FirstOrDefault(pf => pf.ID == request.Id);
            return personPhone;
        }
    }
}
