using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Interfaces.Master
{
    public interface IMSchoolRepository
    {
        Task<List<MSchool>> GetAllAsync();
        Task<MSchool?> GetByIdAsync(long id);
        Task<long> CreateAsync(MSchool school);
        Task<bool> UpdateAsync(long id, MSchool school);
        Task<bool> DeleteAsync(long id, string authDel);

        Task<bool> UpdateLogoAsync(long id, byte[] logo, string logoName, string logoType, string authLstEdt);
    }
}