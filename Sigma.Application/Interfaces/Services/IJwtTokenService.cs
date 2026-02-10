using Sigma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Services
{

    public interface IJwtTokenService
    {
        string GenerateToken(AuthUser user);
    }
}
