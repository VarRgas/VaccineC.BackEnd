﻿using MediatR;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid ID;
        public Guid PersonID;
        public string Email;
        public string Password;
        public string Situation;
        public string FunctionUser;
        public DateTime Register;

        public UpdateUserCommand(Guid id, Guid personId, string email, string password, string situation, string functionUser, DateTime register)
        {
            ID = id;
            PersonID = personId;
            Email = email;
            Password = password;
            Situation = situation;
            FunctionUser = functionUser;
            Register = register;
        }
    }
}
