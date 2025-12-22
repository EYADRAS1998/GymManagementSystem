using Common.Events;
using Common.Messaging;
using MembersService.Infrastructure.Messaging;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.EventHandlers
{

    public class MemberCreatedEventHandler : IEventHandler<MemberCreatedEvent>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;

        public MemberCreatedEventHandler(
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork,
    IEventPublisher eventPublisher)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }
        public async Task HandleAsync(MemberCreatedEvent @event)
        {
            // إنشاء الاشتراك وفق بيانات المستخدم
            var subscription = new Subscription
            {
                Id = Guid.NewGuid(),
                MemberId = @event.MemberId,
                PlanId = @event.PlanId,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                IsActive = @event.IsActive,
                CreatedBy = @event.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            await _subscriptionRepository.AddAsync(subscription);
            await _unitOfWork.SaveChangesAsync();

            // نشر SubscriptionCreatedEvent للدفعات
            var subscriptionCreatedEvent = new SubscriptionCreatedEvent(
                subscription.Id,
                subscription.MemberId,
                subscription.PlanId,
                @event.TotalAmount,
                subscription.StartDate,
                subscription.EndDate,
                subscription.IsActive,
                @event.IsInstallment,
                @event.Currency
            );

            await _eventPublisher.PublishAsync(subscriptionCreatedEvent);
        }

    }

}
