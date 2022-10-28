using AutoMapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;


namespace VaccineC.Query.Application.Queries.Application
{
    public class GetSipniImunizationByIdQueryHandler : IRequestHandler<GetSipniImunizationByIdQuery, SipniImunizationViewModel>
    {
        private readonly IMediator _mediator;
        private readonly VaccineCContext _context;

        public GetSipniImunizationByIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<SipniImunizationViewModel> Handle(GetSipniImunizationByIdQuery request, CancellationToken cancellationToken)
        {

            const string Url = "https://6358380ec26aac906f3e7f5d.mockapi.io/sipni/v1/";
            SipniImunizationViewModel sivm = new SipniImunizationViewModel();

            var sipniIntegrationId = (from a in _context.Applications
                                      where a.ID.Equals(request.ApplicationId)
                                      select a.SipniIntegrationId).FirstOrDefault();

            if (sipniIntegrationId == null) {
                throw new ArgumentException("Integração não encontrada!");
            }

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Url);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await cliente.GetAsync("imunization/"+ sipniIntegrationId);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var returnId = JObject.Parse(responseString)["id"].ToString();
                    var comunicationDate = JObject.Parse(responseString)["resource"]["date"].ToString();
                    var pacientDocument = JObject.Parse(responseString)["resource"]["vaccineCode"]["patient"]["identifier"]["value"].ToString();
                    var authorDocument = JObject.Parse(responseString)["resource"]["author"]["identifier"]["value"].ToString();

                    
                    sivm = new SipniImunizationViewModel
                    {
                        SipniIntegrationId = returnId,
                        ComunicationDate = comunicationDate,
                        AuthorDocument = authorDocument,
                        PacientDocument = pacientDocument,
                        Situation = "Comunicado"

                    };
                }
            }

            return sivm;
        }
    }
}
