using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class AddUserResourceCommandHandler : IRequestHandler<AddUserResourceCommand, UserResourceViewModel>
    {

        private readonly IUserResourceRepository _userResourceRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddUserResourceCommandHandler(IUserResourceRepository userResourceRepository, VaccineCCommandContext ctx)
        {
            _userResourceRepository = userResourceRepository;
            _ctx = ctx;
        }

        public async Task<UserResourceViewModel> Handle(AddUserResourceCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.UserResource newUserResource = new Domain.Entities.UserResource(
                Guid.NewGuid(),
                request.UsersId,
                request.ResourcesId,
                DateTime.Now
            );

            _userResourceRepository.Add(newUserResource);
            await _userResourceRepository.SaveChangesAsync();

            return new UserResourceViewModel()
            {
                ID = newUserResource.ID,
                UsersId = newUserResource.UsersId,
                ResourcesId = newUserResource.ResourcesId,
            };
        }
    }
}
