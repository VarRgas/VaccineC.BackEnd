using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Security;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = _userRepository.GetById(request.ID);
            user.SetPersonId(request.PersonID);
            user.SetEmail(request.Email);
            user.SetFunctionUser(request.FunctionUser);
            user.SetRegister(DateTime.Now);

            await _userRepository.SaveChangesAsync();

            return user.ID;

        }
    }
}
