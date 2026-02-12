using Sigma.Application.DTOs.Master;
using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IClassSectionRepository
    {
        Task<IEnumerable<ClassSection>> GetAllAsync();
        Task<ClassSection?> GetByIdAsync(long id);
        Task<ClassSection?> GetByClassAndSectionAsync(long classId, long sectionId);
        Task<long> AddAsync(ClassSection entity);
        Task UpdateAsync(ClassSection entity);
        Task SoftDeleteAsync(ClassSection entity);

        // 🔹 JOIN Methods
        Task<IEnumerable<ClassSectionResponseDto>> GetAllWithNamesAsync();
        Task<ClassSectionResponseDto?> GetByIdWithNamesAsync(long id);

    }
}
