using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Domain.Abstractions.Repositories
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
    }
}
