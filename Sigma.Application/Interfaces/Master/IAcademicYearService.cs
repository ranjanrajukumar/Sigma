using Sigma.Application.DTOs.Master;
using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IAcademicYearService
    {
        Task<IEnumerable<AcademicYear>> GetAllAsync();

        Task<AcademicYear?> GetByIdAsync(long id);

        Task<string> CreateAsync(AcademicYearCreateDto dto);

        Task<string> UpdateAsync(AcademicYearUpdateDto dto);

        Task<string> DeleteAsync(long id);
    }
}
