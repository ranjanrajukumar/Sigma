using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces
{
    public interface IAuthUserRepository
    {
        Task<AuthUser?> GetByEmailAsync(string email);
        Task UpdateLoginSuccessAsync(long userId, string ipAddress);
        Task UpdateLoginFailAsync(long userId);
    }
}
