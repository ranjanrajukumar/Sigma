using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IMClassRepository
    {
        Task<IEnumerable<MClass>> GetAllAsync();
        Task<MClass?> GetByIdAsync(long id);
        Task<long> CreateAsync(MClass entity);
        Task<bool> UpdateAsync(MClass entity);
        Task<bool> DeleteAsync(long id);
    }

}
