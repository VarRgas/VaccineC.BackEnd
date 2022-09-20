using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Models;
using System.Globalization;
using System;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class ManageNotificationsCommandHandler : IRequestHandler<ManageNotificationsCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public ManageNotificationsCommandHandler(INotificationRepository notificationRepository, IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _notificationRepository = notificationRepository;
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(ManageNotificationsCommand request, CancellationToken cancellationToken)
        {

            await deleteAllPreviousReadedNotifications();

            List<Guid> listUsersId = usersToBeNotified();

            if (listUsersId.Count() > 0)
            {
                var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();

                if (productsSummariesBatches.Count > 0) {
                    var productsSummariesBatchesExpired = allBatchesExpired(productsSummariesBatches);
                    var productsSummariesBatchesCloseToExpire = allBatchesCloseToExpire(productsSummariesBatches);

                    if (productsSummariesBatchesExpired.Count() > 0)
                    {
                        await manageNotificationsExpired(productsSummariesBatchesExpired, listUsersId);
                    }

                    if (productsSummariesBatchesCloseToExpire.Count() > 0)
                    {
                        await manageNotificationsCloseToExpire(productsSummariesBatchesCloseToExpire, listUsersId);
                    }
                }
 
            }

            return Unit.Value;
        }

        private async Task<Boolean> deleteAllPreviousReadedNotifications()
        {
            List<Query.Model.Models.Notification> notificationsReaded = (from n in _context.Notifications
                                                                         where n.Situation == "L"
                                                                         && n.Register.Date < DateTime.Now.Date
                                                                         select n).ToList();
            if (notificationsReaded.Count() > 0) {
                _context.RemoveRange(notificationsReaded);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        private List<ProductSummaryBatchViewModel> allBatchesExpired(List<Query.Model.Models.ProductSummaryBatch> productsSummariesBatches)
        {
            var productsSummariesBatchesViewModel = productsSummariesBatches
                .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                .Where(r => r.NumberOfUnitsBatch > 0 && r.ValidityBatchDate < DateTime.Now)
                .OrderBy(r => r.ValidityBatchDate)
                .ToList();

            return productsSummariesBatchesViewModel;
        }

        private List<ProductSummaryBatchViewModel> allBatchesCloseToExpire(List<Query.Model.Models.ProductSummaryBatch> productsSummariesBatches)
        {
            var productsSummariesBatchesViewModel = productsSummariesBatches
                .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                .Where(r => r.NumberOfUnitsBatch > 0 && r.ValidityBatchDate.Month == DateTime.Now.Month && r.ValidityBatchDate.Year == DateTime.Now.Year)
                .OrderBy(r => r.ValidityBatchDate)
                .ToList();

            return productsSummariesBatchesViewModel;
        }

        private async Task<Boolean> manageNotificationsExpired(List<ProductSummaryBatchViewModel> productsSummariesBatchesExpired, List<Guid> listUsersId)
        {

            foreach (var productSummaryBatch in productsSummariesBatchesExpired)
            {
                await createNotificationsExpired(productSummaryBatch, listUsersId);
            }

            return true;

        }

        private async Task<Boolean> manageNotificationsCloseToExpire(List<ProductSummaryBatchViewModel> productsSummariesBatchesCloseToExpire, List<Guid> listUsersId)
        {

            foreach (var productSummaryBatch in productsSummariesBatchesCloseToExpire)
            {
                await createNotificationsCloseToExpire(productSummaryBatch, listUsersId);
            }

            return true;

        }

        private async Task<Boolean> createNotificationsExpired(ProductSummaryBatchViewModel productSummaryBatch, List<Guid> listUsersId)
        {

            if (listUsersId.Count() > 0)
            {

                foreach (var userId in listUsersId)
                {

                    int notifications = (from n in _context.Notifications
                                         where n.UserId == userId
                                         && n.Message.Contains(productSummaryBatch.Batch)
                                         && n.MessageType == "P"
                                         select n).Count();

                    if (notifications == 0)
                    {
                        string message = "O Lote " + productSummaryBatch.Batch + " - " + productSummaryBatch.Products.Name + " venceu!";

                        Domain.Entities.Notification newNotification = new Domain.Entities.Notification(
                        Guid.NewGuid(),
                        userId,
                        message,
                        "P",
                        "X",
                        DateTime.Now
                   );
                        _notificationRepository.Add(newNotification);
                        await _notificationRepository.SaveChangesAsync();
                    }
                }
            }

            return true;

        }

        private async Task<Boolean> createNotificationsCloseToExpire(ProductSummaryBatchViewModel productSummaryBatch, List<Guid> listUsersId)
        {
            if (listUsersId.Count() > 0)
            {

                foreach (var userId in listUsersId)
                {

                    var dateFormated = productSummaryBatch.ValidityBatchDate.ToString("dd/MM/yyyy");

                    int notifications = (from n in _context.Notifications
                                         where n.Message.Contains(productSummaryBatch.Batch)
                                         && n.UserId == userId
                                         && n.Message.Contains(dateFormated)
                                         && n.MessageType == "L"
                                         select n).Count();

                    if (notifications == 0)
                    {
                        string message = "O Lote " + productSummaryBatch.Batch + " - " + productSummaryBatch.Products.Name + " vence em " + productSummaryBatch.ValidityBatchDate.ToString("dd/MM/yyyy") + "!";
                        
                        Domain.Entities.Notification newNotification = 
                            new Domain.Entities.Notification(Guid.NewGuid(),
                                                             userId,
                                                             message,
                                                             "L",
                                                             "X",
                                                             DateTime.Now);

                        _notificationRepository.Add(newNotification);
                        await _notificationRepository.SaveChangesAsync();
                    }

                }
            }
            return true;
        }

        private List<Guid> usersToBeNotified()
        {
            List<Guid> listUsersId = (from u in _context.UsersResources
                                      join r in _context.Resources on u.ResourcesId equals r.ID
                                      where r.Name.Equals("SITUAÇÃO ESTOQUE")
                                      select u.UsersId).ToList();

            return listUsersId;
        }
    }
}
