using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Services.Master
{
    public class ClassSectionService : IClassSectionService
    {
        private readonly IClassSectionRepository _repository;

        public ClassSectionService(IClassSectionRepository repository)
        {
            _repository = repository;
        }

        // 🔹 JOIN - Get All With Names
        public async Task<IEnumerable<ClassSectionResponseDto>> GetAllWithNamesAsync()
        {
            return await _repository.GetAllWithNamesAsync();
        }

        // 🔹 JOIN - Get By Id With Names
        public async Task<ClassSectionResponseDto?> GetByIdWithNamesAsync(long classSectionId)
        {
            return await _repository.GetByIdWithNamesAsync(classSectionId);
        }

        // 🔹 Create
        public async Task<long> CreateAsync(ClassSectionCreateDto dto)
        {
            var exists = await _repository
                .GetByClassAndSectionAsync(dto.ClassId, dto.SectionId);

            if (exists != null)
                throw new ApplicationException("Class and Section already mapped.");

            var entity = new ClassSection
            {
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                AuthAdd = dto.AuthAdd,
                AddOnDt = DateTime.UtcNow,
                DelStatus = false
            };

            return await _repository.AddAsync(entity);
        }

        // 🔹 Update
        public async Task<bool> UpdateAsync(ClassSectionUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.ClassSectionId);

            if (existing == null || existing.DelStatus)
                throw new ApplicationException("Mapping not found.");

            existing.ClassId = dto.ClassId;
            existing.SectionId = dto.SectionId;
            existing.AuthLstEdt = dto.AuthLstEdt;
            existing.EditOnDt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);

            return true;
        }

        // 🔹 Soft Delete
        public async Task<bool> DeleteAsync(long classSectionId, string deletedBy)
        {
            var existing = await _repository.GetByIdAsync(classSectionId);

            if (existing == null || existing.DelStatus)
                throw new ApplicationException("Mapping not found.");

            existing.DelStatus = true;
            existing.AuthDel = deletedBy;
            existing.DelOnDt = DateTime.UtcNow;

            await _repository.SoftDeleteAsync(existing);

            return true;
        }
    }
}
