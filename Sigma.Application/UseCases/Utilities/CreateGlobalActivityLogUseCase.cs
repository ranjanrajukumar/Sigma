using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.UseCases.Utilities
{
    public class CreateGlobalActivityLogUseCase
    {
        private readonly IGlobalActivityLogService _service;

        public CreateGlobalActivityLogUseCase(IGlobalActivityLogService service)
        {
            _service = service;
        }

        public async Task ExecuteAsync(GlobalActivityLog log)
        {
            await _service.LogAsync(log);
        }
    }
}
