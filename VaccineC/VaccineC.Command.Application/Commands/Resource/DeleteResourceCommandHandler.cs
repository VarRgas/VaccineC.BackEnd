using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Unit>
    {
        private readonly IResourceRepository _resourceRepository;

        public DeleteResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = _resourceRepository.GetById(request.Id);
            _resourceRepository.Remove(resource);
            await _resourceRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
