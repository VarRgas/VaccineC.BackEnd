using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class AddResourceCommandHandler : IRequestHandler<AddResourceCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;

        public AddResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Guid> Handle(AddResourceCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Resource newResource = new Domain.Entities.Resource(Guid.NewGuid(), request.Name, request.UrlName, DateTime.Now);
            _resourceRepository.Add(newResource);
            await _resourceRepository.SaveChangesAsync();
            return newResource.ID;
        }

    }
}
