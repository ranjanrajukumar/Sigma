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

        Task<AcademicYear?> GetAcademicYearWithTerms(long id);

        Task<long> CreateAsync(AcademicYear entity);

        Task<bool> UpdateAsync(AcademicYear entity);

        Task<bool> DeleteAsync(long id);

        Task<bool> ExistsAsync(string academicYear);

        Task InsertTermsAsync(List<AcademicYearTerm> terms);

        Task UpdateTermAsync(AcademicYearTerm term);

        Task InsertTermAsync(AcademicYearTerm term);

        Task<IEnumerable<AcademicYear>> GetAllWithTermsAsync();

    }
}
