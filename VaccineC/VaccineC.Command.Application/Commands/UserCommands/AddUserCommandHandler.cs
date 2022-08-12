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
        private readonly IPersonRepository _personRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddUserCommandHandler(IUserRepository userRepository, IPersonRepository personRepository, VaccineCCommandContext ctx)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _ctx = ctx;
        }

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var t = await _ctx.Users.FirstOrDefaultAsync();

            var person = new Person()
            {
                ID = Guid.NewGuid(),
                PersonType = request.PersonType,
                Name = request.Name,
                Register = request.Register,

            };

            var user = new User()
            {
                ID = Guid.NewGuid(),
                PersonId = person.ID,
                Email = request.Email,
                Password = PasswordManager.HashPassword(request.Password),
                Situation = request.Situation,
            };

            _personRepository.Add(person);
            _userRepository.Add(user);
            _userRepository.SaveChangesAsync();

            return user.ID;
        }
    }
}
