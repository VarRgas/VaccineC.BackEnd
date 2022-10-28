using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class SipniImunizationViewModel
    {
        public string? SipniIntegrationId { get; set; }
        public string AuthorDocument { get; set; }
        public string PacientDocument { get; set; }
        public string ComunicationDate { get; set; }
        public string Situation { get; set; }
    }
}
