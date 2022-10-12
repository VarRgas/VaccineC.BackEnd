using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VaccineC.Command.Application.Commands.AuthorizationNotification
{
    public class SendMessageSMSCommandHandler : IRequestHandler<SendMessageSMSCommand, Unit>
    {
        private readonly IAuthorizationNotificationRepository _repository;
        private static readonly HttpClient client = new HttpClient();

        public SendMessageSMSCommandHandler(IAuthorizationNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SendMessageSMSCommand request, CancellationToken cancellationToken)
        {
            string url = "https://api.smsdev.com.br/v1/send";
            string key = "M30A09QH6Z80WHY0DFS9QECUBIBUVBVT67P50CY9BYSL54W6A504FO9XLB5VLLAD7Y6WUW9PELVVI90LNCYA05RSJU0LY9MIXYIZ06VOQVZXXAJ9N45LQ25QS7IS5V7B";
            string type = "9";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["key"] = key;
                data["type"] = type;
                data["number"] = request.Number;
                data["msg"] = request.Message;
                data["jobdate"] = request.JobDate;
                data["jobtime"] = request.JobTime;

                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                var returnId = JObject.Parse(responseInString)["id"].ToString();

                var authorizationNotification = _repository.GetById(request.AuthorizationNotificationId);

                if (authorizationNotification == null)
                {
                    throw new ArgumentException("Autorização Notificação não encontrada!");
                }

                authorizationNotification.SetReturnId(returnId);
                await _repository.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
