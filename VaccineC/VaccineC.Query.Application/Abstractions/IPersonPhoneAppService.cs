using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonPhoneAppService
    {
        Task<IEnumerable<PersonPhoneViewModel>> GetAllAsync();
        Task<IEnumerable<PersonPhoneViewModel>> GetAllPersonsPhonesByPersonId(Guid personId);
        Task<IEnumerable<PersonPhoneViewModel>> GetAllPersonsPhonesCelByPersonId(Guid personId);
        PersonPhoneViewModel GetById(Guid id);
    }
}
