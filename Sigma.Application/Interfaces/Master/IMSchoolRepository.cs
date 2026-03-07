using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IMSchoolRepository
    {
        Task<List<MSchool>> GetAllAsync();
        Task<MSchool?> GetByIdAsync(long id);
        Task<long> CreateAsync(MSchool school);
        Task<bool> UpdateAsync(long id, MSchool school);
        Task<bool> DeleteAsync(long id, string authDel);
    }
}
