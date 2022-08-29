﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Security;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = _userRepository.GetById(request.ID);
            user.SetPersonId(request.PersonID);
            user.SetEmail(request.Email);
            user.SetFunctionUser(request.FunctionUser);
            user.SetSituation(request.Situation);
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