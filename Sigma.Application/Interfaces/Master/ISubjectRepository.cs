using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetSubjects();
        Task<Subject> GetSubjectById(long id);
        Task<long> CreateSubject(Subject subject);
        Task<bool> UpdateSubject(Subject subject);
        Task<bool> DeleteSubject(long id);
    }

}
