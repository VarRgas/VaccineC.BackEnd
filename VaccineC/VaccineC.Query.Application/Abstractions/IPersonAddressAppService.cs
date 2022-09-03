using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonAddressAppService
    {
        Task<IEnumerable<PersonAddressViewModel>> GetAllAsync();
        Task<IEnumerable<PersonAddressViewModel>> GetAllPersonsAddressesByPersonId(Guid personId);
        PersonAddressViewModel GetById(Guid id);
    }
}
