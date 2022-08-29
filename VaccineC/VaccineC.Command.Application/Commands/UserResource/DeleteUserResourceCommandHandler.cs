using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class DeleteUserResourceCommandHandler : IRequestHandler<DeleteUserResourceCommand, Unit>
    {
        private readonly IUserResourceRepository _userResourceRepository;

        public DeleteUserResourceCommandHandler(IUserResourceRepository userResourceRepository)
        {
            _userResourceRepository = userResourceRepository;
        }

        public async Task<Unit> Handle(DeleteUserResourceCommand request, CancellationToken cancellationToken)
        {
            var userResource = _userResourceRepository.GetById(request.Id);
            _userResourceRepository.Remove(userResource);
            await _userResourceRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
