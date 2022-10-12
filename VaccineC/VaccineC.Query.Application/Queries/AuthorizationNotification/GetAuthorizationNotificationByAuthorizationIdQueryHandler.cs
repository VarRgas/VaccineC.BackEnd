using AutoMapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationByAuthorizationIdQueryHandler : IRequestHandler<GetAuthorizationNotificationByAuthorizationIdQuery, AuthorizationNotificationViewModel>
    {

        private readonly IMediator _mediator;

        public GetAuthorizationNotificationByAuthorizationIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
        }

        public async Task<AuthorizationNotificationViewModel> Handle(GetAuthorizationNotificationByAuthorizationIdQuery request, CancellationToken cancellationToken)
        {
            var authorizationsNotifications = await _mediator.Send(new GetAuthorizationNotificationListQuery());
            var authorizationNotification = authorizationsNotifications.FirstOrDefault(a => a.AuthorizationId == request.AuthorizationId);

            authorizationNotification.ReturnMessage = await getSMSSituation(authorizationNotification.ReturnId);

            return authorizationNotification;
        }

        public async Task<string> getSMSSituation(string returnId)
        {

            string url = "https://api.smsdev.com.br/v1/dlr";
            string key = "M30A09QH6Z80WHY0DFS9QECUBIBUVBVT67P50CY9BYSL54W6A504FO9XLB5VLLAD7Y6WUW9PELVVI90LNCYA05RSJU0LY9MIXYIZ06VOQVZXXAJ9N45LQ25QS7IS5V7B";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["key"] = key;
                data["id"] = returnId;

                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                var situation = JObject.Parse(responseInString)["situacao"].ToString();
                var description = JObject.Parse(responseInString)["descricao"].ToString();

                return situation + " - " + formatDescription(description);
            }

            return "";
        }

        private string formatDescription(string? description)
        {
            if (description.Equals("FILA"))
            {
                return "Mensagem aguardando processamento.";
            } 
            else if (description.Equals("RECEBIDA"))
            {
                return "Mensagem entregue no aparelho do cliente.";
            }
            else if (description.Equals("ENVIADA"))
            {
                return "Mensagem enviada a operadora.";
            }
            else if (description.Equals("ERRO"))
            {
                return "Erro de validação da mensagem.";
            }
            else if (description.Equals("CANCELADA"))
            {
                return "Mensagem cancelada pelo usuário.";
            }
            else if (description.Equals("BLACK LIST"))
            {
                return "Destinatário ativo no grupo ‘Black List’.";
            }
            else
            {
                return "";
            }
        } 
    }
}
