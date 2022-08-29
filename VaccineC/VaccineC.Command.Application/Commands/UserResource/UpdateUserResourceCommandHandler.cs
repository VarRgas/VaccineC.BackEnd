using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class UpdateUserResourceCommandHandler : IRequestHandler<UpdateUserResourceCommand, UserResourceViewModel>
    {

        private readonly IUserResourceRepository _userResourceRepository;
        private readonly VaccineCCommandContext _ctx;

        public UpdateUserResourceCommandHandler(IUserResourceRepository userResourceRepository, VaccineCCommandContext ctx)
        {
            _userResourceRepository = userResourceRepository;
            _ctx = ctx;
        }

        public async Task<UserResourceViewModel> Handle(UpdateUserResourceCommand request, CancellationToken cancellationToken)
        {

            var userResource = _userResourceRepository.GetById(request.ID);
            userResource.SetResourcesId(request.ResourcesId);
            userResource.SetUsersId(request.UsersId);
            userResource.SetRegister(DateTime.Now);

            await _userResourceRepository.SaveChangesAsync();

            return new UserResourceViewModel()
            {
                ID = userResource.ID,
                UsersId = userResource.UsersId,
                ResourcesId = userResource.ResourcesId,
            };

        }
    }
}
