﻿using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class AuthorizationRepository : RepositoryBase<Authorization>, IAuthorizationRepository
    {
        public AuthorizationRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
