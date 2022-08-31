using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateDeactivateUserSituationCommandHandler : IRequestHandler<UpdateDeactivateUserSituationCommand, UserViewModel>
    {

        private readonly IUserRepository _userRepository;

        public UpdateDeactivateUserSituationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(UpdateDeactivateUserSituationCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.ID);
            user.SetSituation("I");
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