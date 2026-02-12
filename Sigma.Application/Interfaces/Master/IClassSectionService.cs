using Sigma.Application.DTOs.Master;

namespace Sigma.Application.Interfaces.Master
{
    public interface IClassSectionService
    {
        // 🔹 JOIN - Get All With Names
        Task<IEnumerable<ClassSectionResponseDto>> GetAllWithNamesAsync();

        // 🔹 JOIN - Get By Id With Names
        Task<ClassSectionResponseDto?> GetByIdWithNamesAsync(long classSectionId);

        // 🔹 Create Mapping
        Task<long> CreateAsync(ClassSectionCreateDto dto);

        // 🔹 Update Mapping
        Task<bool> UpdateAsync(ClassSectionUpdateDto dto);

        // 🔹 Soft Delete
        Task<bool> DeleteAsync(long classSectionId, string deletedBy);
    }
}
