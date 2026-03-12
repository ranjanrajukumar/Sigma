using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IMClassTeacherRepository
    {
      
            Task<IEnumerable<MClassTeacher>> GetAllAsync();
            Task<MClassTeacher?> GetByIdAsync(long id);
            Task<long> CreateAsync(MClassTeacher entity);
            Task<bool> UpdateAsync(MClassTeacher entity);
            Task<bool> DeleteAsync(long id, string deletedBy);
        
    }
}
