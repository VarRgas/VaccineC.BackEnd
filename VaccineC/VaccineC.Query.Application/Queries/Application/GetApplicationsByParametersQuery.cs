using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByParametersQuery : IRequest<IEnumerable<PersonViewModel>>
    {
        public Guid ResponsibleId;
        public DateTime ApplicationDate;
        public string PersonName;

        public GetApplicationsByParametersQuery(Guid responsibleId, DateTime applicationDate, string personName)
        {
            ResponsibleId = responsibleId;
            ApplicationDate = applicationDate;
            PersonName = personName;
        }
    }
}
