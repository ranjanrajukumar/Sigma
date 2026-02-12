using Sigma.Application.DTOs.Master;
using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IAcademicYearService
    {
        /// <summary>
        /// Get all academic years (excluding deleted)
        /// </summary>
        Task<IEnumerable<AcademicYear>> GetAllAsync();

        /// <summary>
        /// Get academic year by Id
        /// </summary>
        Task<AcademicYear?> GetByIdAsync(long id);

        /// <summary>
        /// Create new academic year
        /// </summary>
        Task<string> CreateAsync(AcademicYearCreateDto dto);

        /// <summary>
        /// Update academic year
        /// </summary>
        Task<string> UpdateAsync(AcademicYearUpdateDto dto);

        /// <summary>
        /// Soft delete academic year
        /// </summary>
        Task<string> DeleteAsync(long id);
    }
}
