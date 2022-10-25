using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Application
{
    public class AddApplicationCommandHandler : IRequestHandler<AddApplicationCommand, IEnumerable<ApplicationAvailableViewModel>>
    {
        private readonly IApplicationRepository _repository;
        private readonly IMovementRepository _movementRepository;
        private readonly IMovementProductRepository _movementProductRepository;
        private readonly IProductSummaryBatchRepository _productSummaryBatchRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IApplicationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly VaccineCContext _context;
        private readonly IMediator _mediator;

        public AddApplicationCommandHandler(IApplicationRepository repository, IApplicationAppService appService, VaccineCCommandContext ctx, IMediator mediator, IMovementRepository movementRepository, IMovementProductRepository movementProductRepository, IProductSummaryBatchRepository productSummaryBatchRepository, VaccineCContext context, IAuthorizationRepository authorizationRepository)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
            _movementRepository = movementRepository;
            _movementProductRepository = movementProductRepository;
            _productSummaryBatchRepository = productSummaryBatchRepository;
            _context = context;
            _authorizationRepository = authorizationRepository;
        }

        public async Task<IEnumerable<ApplicationAvailableViewModel>> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
        {
            DateTime applicationDate = (DateTime)request.ApplicationDate;
            var applicationDateFormated = TimeZoneInfo.ConvertTime(applicationDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            await validateApplicationDate(applicationDateFormated);
            await validateBorrowerData(request.AuthorizationId);
            await validateUserData(request.UserId);

            Domain.Entities.Application newApplication = new Domain.Entities.Application(
                Guid.NewGuid(),
                request.UserId,
                request.BudgetProductId,
                applicationDateFormated,
                request.DoseType,
                request.RouteOfAdministration,
                request.ApplicationPlace,
                DateTime.Now,
                request.ProductSummaryBatchId,
                request.AuthorizationId
                );

            _repository.Add(newApplication);
            await _repository.SaveChangesAsync();

            await createMovementBatch(newApplication);
            await updateAuthorizationSituation(newApplication);

            var applicationBorrowerId = (from a in _context.Authorizations
                                           join p in _context.Persons on a.BorrowerPersonId equals p.ID
                                           where a.ID.Equals(newApplication.AuthorizationId)
                                           select p.ID).FirstOrDefault();

            return await _appService.GetAvailableApplicationsByPersonId(applicationBorrowerId);

        }

        private async Task<Unit> validateUserData(Guid userId)
        {
            var pfUser = (from u in _context.Users
                             join p in _context.Persons on u.PersonId equals p.ID
                             join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                             where u.ID.Equals(userId)
                             select pf).FirstOrDefault();

            if (pfUser == null)
            {
                throw new ArgumentException("O Usuário aplicador não possui informações complementares cadastradas para prosseguir com a aplicação!");
            }

            if (pfUser.CnsNumber.Equals("") || pfUser.CnsNumber.Equals(null)) {
                if (pfUser.CpfNumber.Equals("") || pfUser.CpfNumber.Equals(null))
                {
                    throw new ArgumentException("O Usuário aplicador deve possuir um CPF e/ou CNS cadastrados para realizar a aplicação!");
                }
            }

            return Unit.Value;
        }

        private async Task<Unit> validateBorrowerData(Guid authorizationId)
        {
            var pfBorrower = (from a in _context.Authorizations
                          join p in _context.Persons on a.BorrowerPersonId equals p.ID
                          join pf in _context.PersonsPhysical on p.ID equals pf.PersonID
                          where a.ID.Equals(authorizationId)
                          select pf).FirstOrDefault();

            if (pfBorrower == null) {
                throw new ArgumentException("O Tomador não possui informações complementares cadastradas para prosseguir com a aplicação!");
            }

            if (pfBorrower.CnsNumber.Equals("") || pfBorrower.CnsNumber.Equals(null))
            {
                if (pfBorrower.CpfNumber.Equals("") || pfBorrower.CpfNumber.Equals(null))
                {
                    throw new ArgumentException("O Tomador deve possuir um CPF e/ou CNS cadastrados para receber a aplicação!");
                }
            }

            return Unit.Value;
        }

        private async Task<Unit> validateApplicationDate(DateTime applicationDateFormated)
        {
            DateTime now = DateTime.Now;

            if (applicationDateFormated.Date > now.Date)
            {
                throw new ArgumentException("A Data de aplicação não pode ser maior que a data atual!");
            }

            return Unit.Value;
        }

        private async Task<Unit> updateAuthorizationSituation(Domain.Entities.Application application)
        {
            var authorization = _authorizationRepository.GetById(application.AuthorizationId);

            if (authorization == null) {
                throw new ArgumentException("Autorização não encontrada!");
            }
            
            authorization.SetSituation("P");
            authorization.SetRegister(DateTime.Now);
            await _authorizationRepository.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Unit> createMovementBatch(Domain.Entities.Application application)
        {

            Domain.Entities.Movement newMovement = new Domain.Entities.Movement(
                Guid.NewGuid(),
                application.UserId,
                "S",
                0,
                DateTime.Now,
                "F"
                );

            _movementRepository.Add(newMovement);
            await _movementRepository.SaveChangesAsync();

            var productSummaryBatch = _productSummaryBatchRepository.GetById((Guid)application.ProductSummaryBatchId);
            await validateProductSummaryBatch(productSummaryBatch);

            DateTime applicationDate = (DateTime)application.ApplicationDate;
            var applicationDateConverted = TimeZoneInfo.ConvertTime(applicationDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            string applicationDateFormated = applicationDateConverted.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var applicationBorrowerName = (from a in _context.Authorizations
                                       join p in _context.Persons on a.BorrowerPersonId equals p.ID
                                       where a.ID.Equals(application.AuthorizationId)
                                       select p.Name).FirstOrDefault();

            Domain.Entities.MovementProduct newMovementProduct = new Domain.Entities.MovementProduct(
                Guid.NewGuid(),
                newMovement.ID,
                productSummaryBatch.ProductsId,
                productSummaryBatch.Batch,
                1,
                0,
                0,
                "Unidade removida do lote " + productSummaryBatch.Batch + " após aplicação realizada no dia " + applicationDateFormated + " para " + applicationBorrowerName + ".",
                DateTime.Now,
                productSummaryBatch.ManufacturingDate,
                productSummaryBatch.ValidityBatchDate,
                productSummaryBatch.Manufacturer
                );

            _movementProductRepository.Add(newMovementProduct);
            await _movementProductRepository.SaveChangesAsync();

            productSummaryBatch.SetNumberOfUnitsBatch(productSummaryBatch.NumberOfUnitsBatch - 1);
            productSummaryBatch.SetRegister(DateTime.Now);
            await _productSummaryBatchRepository.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Unit> validateProductSummaryBatch(Domain.Entities.ProductSummaryBatch productSummaryBatch)
        {
            if (productSummaryBatch == null)
            {
                throw new ArgumentException("Lote não encontrado para o produto!");
            }

            if (productSummaryBatch.NumberOfUnitsBatch < 1)
            {
                throw new ArgumentException("Não é possível retirar 1 unidade do lote " + productSummaryBatch.Batch + ", pois o total de unidades presentes é " + (int)productSummaryBatch.NumberOfUnitsBatch);
            }

            return Unit.Value;
        }
    }
}
