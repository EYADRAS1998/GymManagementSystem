using MembersService.Application.DTOs;
using MembersService.Domain.Entities;
using MembersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Services.Impl
{
    public class MemberProgressService : IMemberProgressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberProgressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(CreateMemberProgressDto dto)
        {
            var progress = new MemberProgress
            {
                Id = Guid.NewGuid(),
                MemberId = dto.MemberId,
                Weight = dto.Weight,
                MeasurementsJson = dto.MeasurementsJson,
                Notes = dto.Notes,
                RecordedBy = dto.RecordedBy,
                DateRecorded = DateTime.UtcNow
            };

            await _unitOfWork.MemberProgress.AddAsync(progress);
            await _unitOfWork.CommitAsync();

            return progress.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.MemberProgress.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<MemberProgressDto> GetByIdAsync(Guid id)
        {
            var progress = await _unitOfWork.MemberProgress.GetByIdAsync(id);
            if (progress == null) return null;

            return new MemberProgressDto
            {
                Id = progress.Id,
                MemberId = progress.MemberId,
                Weight = progress.Weight,
                MeasurementsJson = progress.MeasurementsJson,
                Notes = progress.Notes,
                DateRecorded = progress.DateRecorded,
                RecordedBy = progress.RecordedBy
            };
        }

        public async Task<IEnumerable<MemberProgressDto>> GetByMemberIdAsync(Guid memberId)
        {
            var progresses = await _unitOfWork.MemberProgress.GetByMemberIdAsync(memberId);

            return progresses.Select(p => new MemberProgressDto
            {
                Id = p.Id,
                MemberId = p.MemberId,
                Weight = p.Weight,
                MeasurementsJson = p.MeasurementsJson,
                Notes = p.Notes,
                DateRecorded = p.DateRecorded,
                RecordedBy = p.RecordedBy
            });
        }

        public async Task UpdateAsync(Guid id, UpdateMemberProgressDto dto)
        {
            var progress = await _unitOfWork.MemberProgress.GetByIdAsync(id);
            if (progress == null) throw new Exception("Progress record not found");

            progress.Weight = dto.Weight;
            progress.MeasurementsJson = dto.MeasurementsJson;
            progress.Notes = dto.Notes;
            progress.RecordedBy = dto.RecordedBy;
            progress.DateRecorded = DateTime.UtcNow;

            await _unitOfWork.MemberProgress.UpdateAsync(progress);
            await _unitOfWork.CommitAsync();
        }
    }
}
