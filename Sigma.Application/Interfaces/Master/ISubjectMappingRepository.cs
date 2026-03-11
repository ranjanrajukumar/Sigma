using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface ISubjectMappingRepository
    {
        Task<IEnumerable<SubjectMapping>> GetAllAsync();
        Task<SubjectMapping?> GetByIdAsync(long id);
        Task<long> CreateAsync(SubjectMapping entity);
        Task<bool> UpdateAsync(SubjectMapping entity);
        Task<bool> DeleteAsync(long id, string deletedBy);

    }
}
