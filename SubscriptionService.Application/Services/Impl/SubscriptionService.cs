using AutoMapper;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Services.Impl
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionReadDto> CreateAsync(SubscriptionCreateDto dto, Guid createdBy)
        {
            var subscription = _mapper.Map<Subscription>(dto);
            subscription.Id = Guid.NewGuid();
            subscription.CreatedBy = createdBy;
            subscription.CreatedAt = DateTime.UtcNow;
            subscription.IsActive = true;

            await _unitOfWork.Subscriptions.AddAsync(subscription);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SubscriptionReadDto>(subscription);
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");

            _unitOfWork.Subscriptions.Delete(subscription);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SubscriptionReadDto> GetByIdAsync(Guid id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");

            return _mapper.Map<SubscriptionReadDto>(subscription);
        }

        public async Task<SubscriptionPagedDto> GetAllAsync(int pageNumber, int pageSize)
        {
            var pagedResult = await _unitOfWork.Subscriptions.GetAllAsync(pageNumber, pageSize);
            var itemsDto = _mapper.Map<IEnumerable<SubscriptionReadDto>>(pagedResult.Items);

            return new SubscriptionPagedDto
            {
                Items = itemsDto,
                TotalCount = pagedResult.TotalCount
            };
        }

        public async Task<SubscriptionPagedDto> GetByMemberIdAsync(Guid memberId, int pageNumber, int pageSize)
        {
            var pagedResult = await _unitOfWork.Subscriptions.GetByMemberIdAsync(memberId, pageNumber, pageSize);
            var itemsDto = _mapper.Map<IEnumerable<SubscriptionReadDto>>(pagedResult.Items);

            return new SubscriptionPagedDto
            {
                Items = itemsDto,
                TotalCount = pagedResult.TotalCount
            };
        }

        public async Task<SubscriptionReadDto> UpdateAsync(Guid id, SubscriptionUpdateDto dto)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");

            _mapper.Map(dto, subscription);
            _unitOfWork.Subscriptions.Update(subscription);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SubscriptionReadDto>(subscription);
        }

        public async Task CancelAsync(Guid subscriptionId)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(subscriptionId);
            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");
            subscription.Cancel(); // Domain logic

            _unitOfWork.Subscriptions.Update(subscription);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
