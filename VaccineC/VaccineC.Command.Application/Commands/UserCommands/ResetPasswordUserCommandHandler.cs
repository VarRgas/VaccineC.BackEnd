using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Security;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class ResetPasswordUserCommandHandler : IRequestHandler<ResetPasswordUserCommand, UserViewModel>
    {

        private readonly IUserRepository _userRepository;

        public ResetPasswordUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(ResetPasswordUserCommand request, CancellationToken cancellationToken)
        {

            var user = _userRepository.GetById(request.ID);
            user.SetPassword(PasswordManager.HashPassword(request.Password));
            user.SetRegister(DateTime.Now);

            await _userRepository.SaveChangesAsync();

            return new UserViewModel()
            {
                ID = user.ID,
                Email = user.Email,
                Password = user.Password,
                Situation = user.Situation,
                FunctionUser = user.FunctionUser,
                PersonId = user.PersonId
            };
        }
    }
}
