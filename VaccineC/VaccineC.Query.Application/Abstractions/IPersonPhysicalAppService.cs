using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonPhysicalAppService
    {
        Task<IEnumerable<PersonsPhysicalViewModel>> GetAllAsync();
        Task<IEnumerable<PersonsPhysicalViewModel>> GetAllPhysicalComplementsByPersonId(Guid personId);
    }
}
