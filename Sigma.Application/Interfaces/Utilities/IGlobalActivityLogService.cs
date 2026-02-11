using Sigma.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Utilities
{
    public interface IGlobalActivityLogService
    {
        Task LogAsync(GlobalActivityLog log);
    }
}
