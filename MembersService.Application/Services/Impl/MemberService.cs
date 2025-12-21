using Common;
using MembersService.Application.DTOs;
using MembersService.Domain.Events;
using MembersService.Domain.Repositories;
using MembersService.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Services.Impl
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;


        public MemberService(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> CreateAsync(CreateMemberDto dto)
        {
            var member = new Domain.Entities.Member
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Notes = dto.Notes,
                JoinDate = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.CommitAsync();

            // نشر الحدث
            var memberCreatedEvent = new MemberCreatedEvent(
                member.Id,
                member.FullName,
                member.PhoneNumber,
                member.Email,
                member.BirthDate,
                member.Gender,
                member.Notes,
                member.CreatedBy,
                member.CreatedAt
            );

            await _eventPublisher.PublishAsync(memberCreatedEvent);


            return member.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Members.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<MemberDto> GetByIdAsync(Guid id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return null;

            return new MemberDto
            {
                Id = member.Id,
                FullName = member.FullName,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                BirthDate = member.BirthDate,
                Gender = member.Gender,
                JoinDate = member.JoinDate,
                Notes = member.Notes,
                IsActive = member.IsActive
            };
        }

        public async Task<PagedResult<MemberDto>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var paged = await _unitOfWork.Members.GetPagedAsync(pageNumber, pageSize);

            var dtoItems = paged.Items.Select(member => new MemberDto
            {
                Id = member.Id,
                FullName = member.FullName,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                BirthDate = member.BirthDate,
                Gender = member.Gender,
                JoinDate = member.JoinDate,
                Notes = member.Notes,
                IsActive = member.IsActive
            }).ToList();

            return new PagedResult<MemberDto>(dtoItems, paged.TotalCount);
        }

        public async Task UpdateAsync(Guid id, UpdateMemberDto dto)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) throw new Exception("Member not found");

            member.FullName = dto.FullName;
            member.PhoneNumber = dto.PhoneNumber;
            member.Email = dto.Email;
            member.BirthDate = dto.BirthDate;
            member.Gender = dto.Gender;
            member.Notes = dto.Notes;
            member.IsActive = dto.IsActive;
            member.UpdatedBy = dto.UpdatedBy;
            member.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Members.UpdateAsync(member);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await _unitOfWork.Members.GetActiveCountAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _unitOfWork.Members.GetTotalCountAsync();
        }
    }
}
