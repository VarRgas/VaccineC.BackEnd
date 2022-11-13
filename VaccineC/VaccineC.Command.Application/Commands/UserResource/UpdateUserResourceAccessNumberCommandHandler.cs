using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class UpdateUserResourceAccessNumberCommandHandler : IRequestHandler<UpdateUserResourceAccessNumberCommand, Unit>
    {

        private readonly IUserResourceRepository _userResourceRepository;
        private readonly IResourceAppService _resourceAppService;
        private readonly VaccineCCommandContext _ctx;
        private readonly VaccineCContext _context;

        public UpdateUserResourceAccessNumberCommandHandler(IUserResourceRepository userResourceRepository, IResourceAppService resourceAppService, VaccineCCommandContext ctx, VaccineCContext context)
        {
            _userResourceRepository = userResourceRepository;
            _resourceAppService = resourceAppService;
            _ctx = ctx;
            _context = context; 
        }

        public async Task<Unit> Handle(UpdateUserResourceAccessNumberCommand request, CancellationToken cancellationToken)
        {
            var userResources = (from ur in _context.UsersResources
                                 join r in _context.Resources on ur.ResourcesId equals r.ID
                                 where ur.UsersId.Equals(request.UserId)
                                 where r.UrlName.ToLower().Contains(request.UrlName.ToLower())
                                 select ur).FirstOrDefault();

            if(userResources != null)
            {
                var userResource = _userResourceRepository.GetById(userResources.ID);
                userResource.SetAccessNumber(userResource.AccessNumber+1);

                await _userResourceRepository.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
