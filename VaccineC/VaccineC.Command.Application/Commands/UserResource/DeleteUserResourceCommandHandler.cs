using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class DeleteUserResourceCommandHandler : IRequestHandler<DeleteUserResourceCommand, IEnumerable<ResourceViewModel>>
    {
        private readonly IUserResourceRepository _userResourceRepository;
        private readonly IResourceAppService _resourceAppService;
        private readonly IUserResourceAppService _userResourceAppService;


        public DeleteUserResourceCommandHandler(IUserResourceRepository userResourceRepository, IResourceAppService resourceAppService, IUserResourceAppService userResourceAppService)
        {
            _userResourceRepository = userResourceRepository;
            _resourceAppService = resourceAppService;
            _userResourceAppService = userResourceAppService;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(DeleteUserResourceCommand request, CancellationToken cancellationToken)
        {
            var userResource = _userResourceRepository.GetById(request.Id);
            _userResourceRepository.Remove(userResource);
            await _userResourceRepository.SaveChangesAsync();

            return await _resourceAppService.GetByUserResource(userResource.UsersId);
        }

    }
}
