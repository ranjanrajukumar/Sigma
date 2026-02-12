using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Interfaces.Services.Master
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository _repo;

        public AcademicYearService(IAcademicYearRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AcademicYear>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<AcademicYear?> GetByIdAsync(long id)
            => await _repo.GetByIdAsync(id);

        public async Task<string> CreateAsync(AcademicYearCreateDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
                return "Start date must be less than End date";

            if (await _repo.ExistsAsync(dto.AcademicYearName))
                return "Academic year already exists";

            var entity = new AcademicYear
            {
                AcademicYearName = dto.AcademicYearName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = false
            };

            await _repo.CreateAsync(entity);
            return "Academic year created successfully";
        }

        public async Task<string> UpdateAsync(AcademicYearUpdateDto dto)
        {
            var entity = new AcademicYear
            {
                AcademicYearId = dto.AcademicYearId,
                AcademicYearName = dto.AcademicYearName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            await _repo.UpdateAsync(entity);
            return "Updated successfully";
        }

        public async Task<string> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return "Deleted successfully";
        }
    }

}
