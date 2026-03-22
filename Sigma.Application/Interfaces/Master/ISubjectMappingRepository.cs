using Sigma.Application.DTOs.Master;
using Sigma.Application.DTOs.Master.Sigma.Application.DTOs.Master;

namespace Sigma.Application.Interfaces.Master
{
    public interface ISubjectMappingRepository
    {
        Task<IEnumerable<SubjectMappingResponseDto>> GetAllAsync();
        Task<SubjectMappingResponseDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(SubjectMappingCreateDto dto);
        Task<bool> UpdateAsync(long id, SubjectMappingCreateDto dto);
        Task<bool> DeleteAsync(long id, string deletedBy);
    }
}