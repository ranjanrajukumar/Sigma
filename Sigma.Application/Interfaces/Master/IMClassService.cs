using Sigma.Application.DTOs.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Interfaces.Master
{
    public interface IMClassService
    {
        /// <summary>
        /// Get all classes (excluding deleted)
        /// </summary>
        Task<IEnumerable<MClass>> GetAllAsync();

        /// <summary>
        /// Get class by Id
        /// </summary>
        Task<MClass?> GetByIdAsync(long id);

        /// <summary>
        /// Create new class
        /// </summary>
        Task<string> CreateAsync(CreateClassDto dto);

        /// <summary>
        /// Update class
        /// </summary>
        Task<string> UpdateAsync(UpdateClassDto dto);

        /// <summary>
        /// Soft delete class
        /// </summary>
        Task<string> DeleteAsync(long id);
    }
}
