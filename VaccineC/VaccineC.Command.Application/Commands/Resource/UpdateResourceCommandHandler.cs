using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Guid>
    {

        private readonly IResourceRepository _resourceRepository;

        public UpdateResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Guid> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = _resourceRepository.GetById(request.ID);
            resource.SetName(request.Name);
            resource.SetUrlName(request.UrlName);
            resource.SetRegister(DateTime.Now);

            await _resourceRepository.SaveChangesAsync();

            return resource.ID;
        }
    }
}
