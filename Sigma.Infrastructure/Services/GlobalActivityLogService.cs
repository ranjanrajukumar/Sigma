using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;
using Sigma.Infrastructure.Persistence.MongoDB;
using Sigma.Infrastructure.Repositories.Utilities;

namespace Sigma.Infrastructure.Services
{
    public class GlobalActivityLogService : IGlobalActivityLogService
    {
        private readonly GlobalActivityLogRepository _repository;
        private readonly MongoSequenceService _sequence;

        public GlobalActivityLogService(
            GlobalActivityLogRepository repository,
            MongoSequenceService sequence)
        {
            _repository = repository;
            _sequence = sequence;
        }

        public async Task LogAsync(GlobalActivityLog log)
        {
            log.LogNo = await _sequence.GetNextAsync("global_activity_log");
            log.CreatedAt = DateTime.UtcNow;

            await _repository.InsertAsync(log);
        }
    }
}
