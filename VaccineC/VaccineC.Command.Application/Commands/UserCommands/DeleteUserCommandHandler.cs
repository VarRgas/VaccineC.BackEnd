using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.Id);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado!");
            }

            _userRepository.Remove(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
