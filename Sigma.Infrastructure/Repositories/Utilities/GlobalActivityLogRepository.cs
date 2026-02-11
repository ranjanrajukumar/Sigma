using MongoDB.Driver;
using Sigma.Domain.Entities.Utilities;

namespace Sigma.Infrastructure.Repositories.Utilities
{
    public class GlobalActivityLogRepository
    {
        private readonly IMongoCollection<GlobalActivityLog> _collection;

        public GlobalActivityLogRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<GlobalActivityLog>("global_activity_logs");
        }

        public async Task InsertAsync(GlobalActivityLog log)
        {
            await _collection.InsertOneAsync(log);
        }
    }
}
