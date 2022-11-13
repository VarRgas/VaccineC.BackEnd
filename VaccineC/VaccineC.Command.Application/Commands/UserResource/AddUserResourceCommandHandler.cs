using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class AddUserResourceCommandHandler : IRequestHandler<AddUserResourceCommand, IEnumerable<ResourceViewModel>>
    {

        private readonly IUserResourceRepository _userResourceRepository;
        private readonly IResourceAppService _resourceAppService;
        private readonly VaccineCCommandContext _ctx;

        public AddUserResourceCommandHandler(IUserResourceRepository userResourceRepository, IResourceAppService resourceAppService, VaccineCCommandContext ctx)
        {
            _userResourceRepository = userResourceRepository;
            _resourceAppService = resourceAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(AddUserResourceCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.UserResource newUserResource = new Domain.Entities.UserResource(
                Guid.NewGuid(),
                request.UsersId,
                request.ResourcesId,
                DateTime.Now,
                0
            );

            _userResourceRepository.Add(newUserResource);
            await _userResourceRepository.SaveChangesAsync();

            return await _resourceAppService.GetByUserResource(newUserResource.UsersId);

        }
    }
}
