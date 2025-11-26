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
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PlanReadDto> CreateAsync(PlanCreateDto dto)
        {
            var plan = _mapper.Map<Plan>(dto);
            await _unitOfWork.Plans.AddAsync(plan);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PlanReadDto>(plan);
        }

        public async Task DeleteAsync(Guid id)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            _unitOfWork.Plans.Delete(plan);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlanReadDto>> GetAllAsync()
        {
            var plans = await _unitOfWork.Plans.GetAllAsync();
            return _mapper.Map<IEnumerable<PlanReadDto>>(plans);
        }

        public async Task<PlanReadDto> GetByIdAsync(Guid id)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            return _mapper.Map<PlanReadDto>(plan);
        }

        public async Task<PlanReadDto> UpdateAsync(Guid id, PlanUpdateDto dto)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            _mapper.Map(dto, plan);
            _unitOfWork.Plans.Update(plan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PlanReadDto>(plan);
        }
    }
}
