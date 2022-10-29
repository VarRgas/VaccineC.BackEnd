using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Application
{
    public class AddSipniImunizationCommandHandler : IRequestHandler<AddSipniImunizationCommand, Unit>
    {
        private readonly IApplicationRepository _repository;
        private readonly VaccineCContext _context;


        public AddSipniImunizationCommandHandler(IApplicationRepository repository,VaccineCContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Unit> Handle(AddSipniImunizationCommand request, CancellationToken cancellationToken)
        {

            const string Url = "https://6358380ec26aac906f3e7f5d.mockapi.io/sipni/v1/";

            Domain.Entities.Application application = request.Application;

            string borrowerCnsCpf = getBorrowerCnsCpf(application);
            string userCnsCpf = getUserCnsCpf(application);
            string productVaccineCode = getProductVaccineCode(application);
            string productManufacturerCode = getProductManufacturerCode(application);
            string productBatchName = getProductBatchName(application);
            string productBatchValidity = getProductValidityDate(application);
            string routeOfAdministrationCode = getRouteOfAdministrationCode(application);
            string applicationPlaceCode = getApplicationPlaceCode(application);
            string doseNumberCode = getDoseNumberCode(application);
            string now = DateTime.Now.ToString("yyyy-MM-dd");

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Url);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = new
                {
                    resourceType = "Bundle",
                    meta = new
                    {
                        lastUpdated = now.ToString()
                    },
                    identifier = new
                    {
                        system = "http://www.saude.gov.br/fhir/r4/NamingSystem/BRRNDS-10302910000182A",
                        value = "1234567890"
                    },
                    type = "document",
                    timestamp = now.ToString(),
                    resource = new
                    {
                        resourceType = "Composition",
                        meta = new
                        {
                            profile = "http://www.saude.gov.br/fhir/r4/StructureDefinition/BRRegistroImunobiologicoAdministradoRotina-1.0"
                        },
                        type = new
                        {
                            coding = new
                            {
                                system = "http://www.saude.gov.br/fhir/r4/CodeSystem/BRTipoDocumento",
                                code = "RIA"
                            }
                        },
                        subject = new
                        {
                            identifier = new
                            {
                                system = "http://www.saude.gov.br/fhir/r4/StructureDefinition/BRIndividuo-1.0",
                                value = borrowerCnsCpf
                            }
                        },
                        date = now.ToString(),
                        author = new
                        {
                            identifier = new
                            {
                                system = "http://www.saude.gov.br/fhir/r4/StructureDefinition/BREstabelecimentoSaude-1.0",
                                value = userCnsCpf
                            }
                        },
                        title = "Registro de Imunobiologico Administrado na Rotina",
                        status = "completed",
                        vaccineCode = new
                        {
                            coding = new
                            {
                                system = "http://www.saude.gov.br/fhir/r4/CodeSystem/BRImunobiologico",
                                value = productVaccineCode
                            },
                            patient = new
                            {
                                identifier = new
                                {
                                    system = "http://www.saude.gov.br/fhir/r4/StructureDefinition/BRIndividuo-1.0",
                                    value = borrowerCnsCpf
                                }
                            },
                            occurrenceDateTime = now.ToString(),
                            manufacturer = new
                            {
                                identifier = new
                                {
                                    system = "http://www.saude.gov.br/fhir/r4/CodeSystem/BRFabricantePNI",
                                    value = productManufacturerCode
                                }
                            },
                            lotNumber = productBatchName,
                            expirationDate = productBatchValidity,
                            site = new
                            {
                                coding = new
                                {
                                    system = "http://www.saude.gov.br/fhir/r4/CodeSystem/BRLocalAplicacao",
                                    value = applicationPlaceCode
                                }
                            },
                            route = new
                            {
                                coding = new
                                {
                                    system = "http://www.saude.gov.br/fhir/r4/CodeSystem/BRViaAdministracao",
                                    value = routeOfAdministrationCode
                                }
                            },
                            performer = new
                            {
                                actor = new
                                {
                                    reference = "Practitioner/" + userCnsCpf
                                }
                            },
                            doseNumberString = doseNumberCode
                        }
                    },

                };

                var response = await cliente.PostAsJsonAsync("imunization/", data);

                if (response.IsSuccessStatusCode)
                {

                    var responseString = await response.Content.ReadAsStringAsync();
                    var returnId = JObject.Parse(responseString)["id"].ToString();

                    var applicationUpdated = _repository.GetById(application.ID);
                    applicationUpdated.SetSipniIntegrationId(returnId);
                    await _repository.SaveChangesAsync();

                }
            }

            return Unit.Value;
        }

        private string getProductValidityDate(Domain.Entities.Application application)
        {
            var productBatchValidity = (from psb in _context.ProductsSummariesBatches
                                        where psb.ID.Equals(application.ProductSummaryBatchId)
                                        select psb.ValidityBatchDate).FirstOrDefault();

            if (productBatchValidity.Equals(null) || productBatchValidity.Equals(""))
            {
                throw new ArgumentException("Impossível realizar integração: Validade do Lote não encontrada!");
            }

            return productBatchValidity.ToString();
        }

        private string getProductBatchName(Domain.Entities.Application application)
        {
            var productBatchName = (from psb in _context.ProductsSummariesBatches
                                    where psb.ID.Equals(application.ProductSummaryBatchId)
                                    select psb.Batch).FirstOrDefault();

            if (productBatchName.Equals(null) || productBatchName.Equals(""))
            {
                throw new ArgumentException("Impossível realizar integração: Lote não encontrado!");
            }

            return productBatchName;
        }

        private string getProductVaccineCode(Domain.Entities.Application application)
        {

            //https://simplifier.net/redenacionaldedadosemsaude/imunobiolgico-duplicate-3

            var vaccineCodeSearch = (from bp in _context.BudgetsProducts
                                     join p in _context.Products on bp.ProductId equals p.ID
                                     join sv in _context.SbimVaccines on p.SbimVaccinesId equals sv.ID
                                     where bp.ID.Equals(application.BudgetProductId)
                                     select sv.RndsId).FirstOrDefault();

            if (vaccineCodeSearch.Equals(null) || vaccineCodeSearch.Equals(""))
            {
                throw new ArgumentException("Impossível realizar integração: Produto não encontrado!");
            }

            return vaccineCodeSearch;
        }

        private string getUserCnsCpf(Domain.Entities.Application application)
        {
            string cnsCpf = "";

            var userPf = (from u in _context.Users
                          join p in _context.Persons on u.PersonId equals p.ID
                          join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                          where u.ID.Equals(application.UserId)
                          select pf).FirstOrDefault();

            if (userPf == null)
            {
                throw new ArgumentException("Impossível realizar integração: Usuário Aplicador não possui complementos cadastrados!");
            }

            if (userPf.CnsNumber == null && userPf.CpfNumber == null)
            {
                throw new ArgumentException("Impossível realizar integração: Usuário Aplicador não possui CNS e/ou CPF cadastrados!");
            }

            if (userPf.CnsNumber == null || userPf.CnsNumber.Equals(""))
            {
                cnsCpf = userPf.CpfNumber;
            }
            else
            {
                cnsCpf = userPf.CnsNumber;
            }

            return cnsCpf;
        }

        private string getBorrowerCnsCpf(Domain.Entities.Application application)
        {

            string cnsCpf = "";

            var borrowerPf = (from a in _context.Authorizations
                              join p in _context.Persons on a.BorrowerPersonId equals p.ID
                              join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                              where a.ID.Equals(application.AuthorizationId)
                              select pf).FirstOrDefault();

            if (borrowerPf == null)
            {
                throw new ArgumentException("Impossível realizar integração: Tomador não possui complementos cadastrados!");
            }

            if (borrowerPf.CnsNumber == null && borrowerPf.CpfNumber == null)
            {
                throw new ArgumentException("Impossível realizar integração: Tomador não possui CNS e/ou CPF cadastrados!");
            }

            if (borrowerPf.CnsNumber == null || borrowerPf.CnsNumber.Equals(""))
            {
                cnsCpf = borrowerPf.CpfNumber;
            }
            else
            {
                cnsCpf = borrowerPf.CnsNumber;
            }

            return cnsCpf;

        }

        private string getDoseNumberCode(Domain.Entities.Application application)
        {
            //https://simplifier.net/RedeNacionaldeDadosemSaude/DosedeVacina

            string doseType = application.DoseType;
            string doseTypeReturn = "";

            switch (doseType)
            {
                case "D1":
                    doseTypeReturn = "1";
                    break;
                case "D2":
                    doseTypeReturn = "2";
                    break;
                case "D3":
                    doseTypeReturn = "3";
                    break;
                case "DR":
                    doseTypeReturn = "6";
                    break;
                case "DU":
                    doseTypeReturn = "9";
                    break;
                default:
                    doseTypeReturn = "";
                    break;
            }

            return doseTypeReturn;
        }

        private string getApplicationPlaceCode(Domain.Entities.Application application)
        {
            //https://simplifier.net/QSaude-Operadora-de-Planos/Local%20de%20Aplica%C3%A7%C3%A3o

            string applicationPlaceCode = application.ApplicationPlace;
            string applicationPlaceCodeReturn = "";

            switch (applicationPlaceCode)
            {
                case "00":
                    applicationPlaceCodeReturn = "1";
                    break;
                case "01":
                    applicationPlaceCodeReturn = "2";
                    break;
                case "02":
                    applicationPlaceCodeReturn = "3";
                    break;
                case "03":
                    applicationPlaceCodeReturn = "0";
                    break;
                case "04":
                    applicationPlaceCodeReturn = "7";
                    break;
                case "05":
                    applicationPlaceCodeReturn = "0";
                    break;
                default:
                    applicationPlaceCodeReturn = "0";
                    break;
            }

            return applicationPlaceCodeReturn;
        }
        private string getRouteOfAdministrationCode(Domain.Entities.Application application)
        {
            //https://simplifier.net/QSaude-Operadora-de-Planos/Via%20de%20Administra%C3%A7%C3%A3o

            string routeOfAdministrationCode = application.RouteOfAdministration;
            string routeOfAdministrationCodeReturn = "";

            switch (routeOfAdministrationCode)
            {
                case "O":
                    routeOfAdministrationCodeReturn = "10907";
                    break;
                case "I":
                    routeOfAdministrationCodeReturn = "10885";
                    break;
                case "S":
                    routeOfAdministrationCodeReturn = "10916";
                    break;
                case "M":
                    routeOfAdministrationCodeReturn = "10890";
                    break;
                default:
                    routeOfAdministrationCodeReturn = "";
                    break;
            }

            return routeOfAdministrationCodeReturn;
        }

        private string getProductManufacturerCode(Domain.Entities.Application application)
        {
            //https://simplifier.net/RedeNacionaldeDadosemSaude/Fabricantedoimunobiolgico

            string manufacturerCode = "";

            var manufacturerName = (from psb in _context.ProductsSummariesBatches
                                    where psb.ID.Equals(application.ProductSummaryBatchId)
                                    select psb.Manufacturer).FirstOrDefault();

            if (manufacturerName.Equals(null) || manufacturerName.Equals(""))
            {
                throw new ArgumentException("Impossível realizar integração: Fabricante do Lote não encontrado!");
            }

            switch (manufacturerName.Trim().ToUpper())
            {
                case "IVB":
                    manufacturerCode = "141";
                    break;
                case "PFIZER":
                    manufacturerCode = "142";
                    break;
                case "FIOCRUZ":
                    manufacturerCode = "149";
                    break;
                case "FUNED":
                    manufacturerCode = "150";
                    break;
                case "F.A.P":
                    manufacturerCode = "151";
                    break;
                case "FAP":
                    manufacturerCode = "151";
                    break;
                case "BUTANTAN":
                    manufacturerCode = "152";
                    break;
                case "TECPAR":
                    manufacturerCode = "153";
                    break;
                case "DADO B":
                    manufacturerCode = "156";
                    break;
                case "SMITHKLINE":
                    manufacturerCode = "159";
                    break;
                case "GREENCROSS":
                    manufacturerCode = "160";
                    break;
                case "AVENTIS":
                    manufacturerCode = "161";
                    break;
                case "CHIRON SPA":
                    manufacturerCode = "162";
                    break;
                case "CHIRON":
                    manufacturerCode = "162";
                    break;
                case "SERUM-INDIA":
                    manufacturerCode = "163";
                    break;
                case "SERUM INDIA":
                    manufacturerCode = "163";
                    break;
                case "LGCHEMICAL":
                    manufacturerCode = "164";
                    break;
                case "MERCK":
                    manufacturerCode = "165";
                    break;
                case "BIKEN":
                    manufacturerCode = "166";
                    break;
                case "SEVAC":
                    manufacturerCode = "167";
                    break;
                case "BERNE":
                    manufacturerCode = "168";
                    break;
                case "INCQS":
                    manufacturerCode = "235";
                    break;
                case "BAYER":
                    manufacturerCode = "139";
                    break;
                case "BAXTER":
                    manufacturerCode = "608";
                    break;
                case "CYANAMID":
                    manufacturerCode = "1659";
                    break;
                case "CPPI":
                    manufacturerCode = "1913";
                    break;
                case "IIMUNO":
                    manufacturerCode = "2250";
                    break;
                case "CSL":
                    manufacturerCode = "2260";
                    break;
                case "WYETH":
                    manufacturerCode = "2263";
                    break;
                case "NOVARTIS":
                    manufacturerCode = "2355";
                    break;
                case "HEBER":
                    manufacturerCode = "2560";
                    break;
                case "BERNA":
                    manufacturerCode = "2769";
                    break;
                case "SANPASTEUR":
                    manufacturerCode = "2862";
                    break;
                case "MERIAL":
                    manufacturerCode = "2901";
                    break;
                case "BIOVET":
                    manufacturerCode = "3521";
                    break;
                case "CRUCELL":
                    manufacturerCode = "10534";
                    break;
                case "VIRBAC":
                    manufacturerCode = "11347";
                    break;
                case "GRIFOLS":
                    manufacturerCode = "12551";
                    break;
                case "KEDRION":
                    manufacturerCode = "12572";
                    break;
                case "BIOGENESIS":
                    manufacturerCode = "13219";
                    break;
                case "SHANTHA":
                    manufacturerCode = "13688";
                    break;
                case "BIOFARMA":
                    manufacturerCode = "16763";
                    break;
                case "BIOLOGICAL":
                    manufacturerCode = "17934";
                    break;
                case "INTERVAX":
                    manufacturerCode = "18189";
                    break;
                case "BB-NCIPD":
                    manufacturerCode = "18190";
                    break;
                case "LGLIFE":
                    manufacturerCode = "22942";
                    break;
                case "MERCK-GO":
                    manufacturerCode = "26313";
                    break;
                case "EUBIOLOGI":
                    manufacturerCode = "27816";
                    break;
                case "GSK-BRASIL":
                    manufacturerCode = "27880";
                    break;
                case "KAMADA":
                    manufacturerCode = "28211";
                    break;
                case "SANMEDLEY":
                    manufacturerCode = "28251";
                    break;
                case "VINS":
                    manufacturerCode = "28283";
                    break;
                case "PFIZER-BELGICA":
                    manufacturerCode = "28290";
                    break;
                case "PANACEA":
                    manufacturerCode = "28303";
                    break;
                case "BOEHRINGER":
                    manufacturerCode = "28738";
                    break;
                case "SINOVAC":
                    manufacturerCode = "29501";
                    break;
                case "ASTRAZENECA":
                    manufacturerCode = "29909";
                    break;
                case "EQUIPLEX":
                    manufacturerCode = "30357";
                    break;
                case "JANSSEN":
                    manufacturerCode = "30587";
                    break;
                case "BAV-NORDIC":
                    manufacturerCode = "33502";
                    break;
                case "AMYIN":
                    manufacturerCode = "33713";
                    break;
                case "FLUARIX":
                    manufacturerCode = "101020";
                    break;
                default:
                    manufacturerCode = "";
                    break;
            }

            return manufacturerCode;

        }
    }
}
