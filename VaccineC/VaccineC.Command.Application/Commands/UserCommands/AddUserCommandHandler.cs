using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Security;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddUserCommandHandler(IUserRepository userRepository, VaccineCCommandContext ctx)
        {
            _userRepository = userRepository;
            _ctx = ctx;
        }

        public async Task<UserViewModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            User newUser = new User(
                Guid.NewGuid(),
                request.PersonID,
                request.Email,
                PasswordManager.HashPassword(request.Password),
                request.Situation,
                request.FunctionUser,
                DateTime.Now
            );

            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            return new UserViewModel()
            {
                ID = newUser.ID,
                Email = newUser.Email,
                Password = newUser.Password,
                Situation = newUser.Situation,
                FunctionUser = newUser.FunctionUser,
                PersonId = newUser.PersonId
            };

        }

    }
}
