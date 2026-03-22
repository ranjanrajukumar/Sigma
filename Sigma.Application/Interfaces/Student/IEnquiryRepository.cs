using Sigma.Application.DTOs;
using Sigma.Application.DTOs.Academics;

namespace Sigma.Application.Interfaces.Academics
{
    public interface IEnquiryRepository
    {
        Task<IEnumerable<EnquiryResponseDto>> GetAllAsync();   // ✅ FIXED
        Task<EnquiryResponseDto?> GetByIdAsync(long id);       // ✅ nullable (best practice)
        Task<long> CreateAsync(CreateEnquiryDto dto);
        Task<bool> UpdateAsync(UpdateEnquiryDto dto);
        Task<bool> DeleteAsync(long id, string authDel);
    }
}