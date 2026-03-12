using Sigma.Application.DTOs.Academics;
using Sigma.Domain.Entities.Academics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Academics
{
    public interface IEnquiryRepository
    {
        Task<IEnumerable<Enquiry>> GetAllAsync();
        Task<Enquiry> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateEnquiryDto dto);
        Task<bool> UpdateAsync(UpdateEnquiryDto dto);
        Task<bool> DeleteAsync(long id, string authDel);
    }
}
