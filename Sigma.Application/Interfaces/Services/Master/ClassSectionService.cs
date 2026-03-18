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

        public async Task<IEnumerable<ClassSectionResponseDto>> GetAllWithNamesAsync()
        {
            return await _repository.GetAllWithNamesAsync();
        }

        public async Task<ClassSectionResponseDto?> GetByIdWithNamesAsync(long id)
        {
            return await _repository.GetByIdWithNamesAsync(id);
        }

        public async Task<long> CreateAsync(ClassSectionCreateDto dto)
        {
            var exists = await _repository
                .GetByClassAndSectionAsync(dto.ClassId, dto.SectionId);

            if (exists != null && !exists.DelStatus)
                throw new ApplicationException("This class and section is already mapped.");

            var entity = new ClassSection
            {
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                RoomNumber = dto.RoomNumber,
                Floor = dto.Floor,
                SectionCapacity = dto.SectionCapacity,
                ClassTeacherId = dto.ClassTeacherId,
                AuthAdd = dto.AuthAdd,
                AddOnDt = DateTime.UtcNow,
                DelStatus = false
            };

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(ClassSectionUpdateDto dto)
        {
            var entity = new ClassSection
            {
                ClassSectionId = dto.ClassSectionId,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                RoomNumber = dto.RoomNumber,
                Floor = dto.Floor,
                SectionCapacity = dto.SectionCapacity,
                ClassTeacherId = dto.ClassTeacherId,
                AuthLstEdt = dto.AuthLstEdt,
                EditOnDt = DateTime.UtcNow
            };

            await _repository.UpdateAsync(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(long id, string deletedBy)
        {
            var entity = new ClassSection
            {
                ClassSectionId = id,
                AuthDel = deletedBy,
                DelOnDt = DateTime.UtcNow,
                DelStatus = true
            };

            await _repository.SoftDeleteAsync(entity);

            return true;
        }
    }
}