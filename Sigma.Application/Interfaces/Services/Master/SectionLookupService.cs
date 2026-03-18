using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Repositories.Interfaces;

namespace Sigma.Application.Services.Master
{
    public class SectionLookupService : ISectionLookupService
    {
        private readonly ISectionLookupRepository _repository;

        public SectionLookupService(ISectionLookupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SectionLookupResponseDto>> GetAllAsync()
        {
            var sections = await _repository.GetAllAsync();

            return sections.Select(x => new SectionLookupResponseDto
            {
                SectionId = x.SectionId,
                SectionName = x.SectionName,
                SectionCode = x.SectionCode
            });
        }

        public async Task<SectionLookupResponseDto?> GetByIdAsync(long sectionId)
        {
            var section = await _repository.GetByIdAsync(sectionId);

            if (section == null)
                return null;

            return new SectionLookupResponseDto
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName,
                SectionCode = section.SectionCode
            };
        }

        public async Task<long> CreateAsync(SectionLookupCreateDto dto)
        {
            var entity = new SectionLookup
            {
                SectionName = dto.SectionName,
                SectionCode = dto.SectionCode,
                AuthAdd = dto.AuthAdd,
                AddOnDt = DateTime.UtcNow
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(SectionLookupUpdateDto dto)
        {
            var entity = new SectionLookup
            {
                SectionId = dto.SectionId,
                SectionName = dto.SectionName,
                SectionCode = dto.SectionCode,
                AuthLstEdt = dto.AuthLstEdt,
                EditOnDt = DateTime.UtcNow
            };

            await _repository.UpdateAsync(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(long sectionId, string deletedBy)
        {
            var entity = new SectionLookup
            {
                SectionId = sectionId,
                AuthDel = deletedBy,
                DelOnDt = DateTime.UtcNow,
                DelStatus = true
            };

            await _repository.SoftDeleteAsync(entity);

            return true;
        }
    }
}