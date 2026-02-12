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

        // ✅ 1️⃣ Get All Active Sections
        public async Task<IEnumerable<SectionLookupResponseDto>> GetAllAsync()
        {
            var sections = await _repository.GetAllAsync();

            return sections
                .Where(x => !x.DelStatus)
                .Select(x => new SectionLookupResponseDto
                {
                    SectionId = x.SectionId,
                    SectionName = x.SectionName
                });
        }

        // ✅ 2️⃣ Get By Id
        public async Task<SectionLookupResponseDto?> GetByIdAsync(long sectionId)
        {
            var section = await _repository.GetByIdAsync(sectionId);

            if (section == null || section.DelStatus)
                return null;

            return new SectionLookupResponseDto
            {
                SectionId = section.SectionId,
                SectionName = section.SectionName
            };
        }

        // ✅ 3️⃣ Create
        public async Task<long> CreateAsync(SectionLookupCreateDto dto)
        {
            var existing = await _repository.GetByNameAsync(dto.SectionName);

            if (existing != null)
                throw new ApplicationException("Section name already exists.");

            var entity = new SectionLookup
            {
                SectionName = dto.SectionName,
                AuthAdd = dto.AuthAdd,
                AddOnDt = DateTime.UtcNow,
                DelStatus = false
            };

            var id = await _repository.AddAsync(entity);

            return id; // Better to return DB generated id
        }

        // ✅ 4️⃣ Update
        public async Task<bool> UpdateAsync(SectionLookupUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.SectionId);

            if (existing == null || existing.DelStatus)
                throw new ApplicationException("Section not found.");

            existing.SectionName = dto.SectionName;
            existing.AuthLstEdt = dto.AuthLstEdt;
            existing.EditOnDt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);

            return true;
        }

        // ✅ 5️⃣ Soft Delete
        public async Task<bool> DeleteAsync(long sectionId, string deletedBy)
        {
            var existing = await _repository.GetByIdAsync(sectionId);

            if (existing == null || existing.DelStatus)
                throw new ApplicationException("Section not found.");

            existing.DelStatus = true;
            existing.AuthDel = deletedBy;
            existing.DelOnDt = DateTime.UtcNow;

            await _repository.SoftDeleteAsync(existing);

            return true;
        }
    }
}
