using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class UpdateUserResourceCommandHandler : IRequestHandler<UpdateUserResourceCommand, IEnumerable<ResourceViewModel>>
    {

        private readonly IUserResourceRepository _userResourceRepository;
        private readonly IResourceAppService _resourceAppService;
        private readonly VaccineCCommandContext _ctx;

        public UpdateUserResourceCommandHandler(IUserResourceRepository userResourceRepository, IResourceAppService resourceAppService, VaccineCCommandContext ctx)
        {
            _userResourceRepository = userResourceRepository;
            _resourceAppService = resourceAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(UpdateUserResourceCommand request, CancellationToken cancellationToken)
        {

            var userResource = _userResourceRepository.GetById(request.ID);

            if (userResource == null)
            {
                throw new ArgumentException("Usuário Recurso não encontrado!");
            }

            userResource.SetResourcesId(request.ResourcesId);
            userResource.SetUsersId(request.UsersId);
            userResource.SetRegister(DateTime.Now);

            await _userResourceRepository.SaveChangesAsync();

            return await _resourceAppService.GetByUserResource(userResource.UsersId);

        }
    }
}
