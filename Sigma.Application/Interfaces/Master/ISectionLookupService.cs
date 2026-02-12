using Sigma.Application.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface ISectionLookupService
    {
        // 🔹 Get All Active Records
        Task<IEnumerable<SectionLookupResponseDto>> GetAllAsync();

        // 🔹 Get By Id
        Task<SectionLookupResponseDto?> GetByIdAsync(long sectionId);

        // 🔹 Create
        Task<long> CreateAsync(SectionLookupCreateDto dto);

        // 🔹 Update
        Task<bool> UpdateAsync(SectionLookupUpdateDto dto);

        // 🔹 Soft Delete
        Task<bool> DeleteAsync(long sectionId, string deletedBy);
    }
}
