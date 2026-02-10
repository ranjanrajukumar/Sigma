using Sigma.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<long> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(long userId);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(long userId);
    }
}
