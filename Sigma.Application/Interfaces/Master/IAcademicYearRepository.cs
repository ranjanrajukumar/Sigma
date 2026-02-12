using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IAcademicYearRepository
    {
        Task<IEnumerable<AcademicYear>> GetAllAsync();
        Task<AcademicYear?> GetByIdAsync(long id);
        Task<long> CreateAsync(AcademicYear entity);
        Task<bool> UpdateAsync(AcademicYear entity);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(string academicYear);
    }
}
