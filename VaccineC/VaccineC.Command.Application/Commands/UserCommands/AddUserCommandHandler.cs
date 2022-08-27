using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Security;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddUserCommandHandler(IUserRepository userRepository, VaccineCCommandContext ctx)
        {
            _userRepository = userRepository;
            _ctx = ctx;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            User newUser = new User(
                Guid.NewGuid(),
                request.PersonID,
                request.Email,
                PasswordManager.HashPassword(request.Password),
                request.Situation,
                DateTime.Now
                );

            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();
            return newUser.ID;

        }
    }
}
