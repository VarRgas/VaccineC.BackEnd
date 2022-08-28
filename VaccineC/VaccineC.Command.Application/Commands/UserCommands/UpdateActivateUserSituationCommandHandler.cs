using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateActivateUserSituationCommandHandler : IRequestHandler<UpdateActivateUserSituationCommand, UserViewModel>
    {

        private readonly IUserRepository _userRepository;

        public UpdateActivateUserSituationCommandHandler(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(UpdateActivateUserSituationCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.ID);
            user.SetSituation("A");
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
