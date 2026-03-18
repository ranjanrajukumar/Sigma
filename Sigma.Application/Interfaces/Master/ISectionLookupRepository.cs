using Sigma.Domain.Entities.Master;

namespace Sigma.Infrastructure.Repositories.Interfaces
{
    public interface ISectionLookupRepository
    {
        Task<IEnumerable<SectionLookup>> GetAllAsync();

        Task<SectionLookup?> GetByIdAsync(long id);

        Task<SectionLookup?> GetByNameAsync(string sectionName);

        Task<long> AddAsync(SectionLookup entity);

        Task UpdateAsync(SectionLookup entity);

        Task SoftDeleteAsync(SectionLookup entity);
    }
}