using MongoDB.Bson;
using MongoDB.Driver;

namespace Sigma.Infrastructure.Persistence.MongoDB
{
    public class MongoSequenceService
    {
        private readonly IMongoCollection<BsonDocument> _counter;

        public MongoSequenceService(IMongoDatabase database)
        {
            _counter = database.GetCollection<BsonDocument>("counters");
        }

        public async Task<long> GetNextAsync(string key)
        {
            var result = await _counter.FindOneAndUpdateAsync(
                Builders<BsonDocument>.Filter.Eq("_id", key),
                Builders<BsonDocument>.Update.Inc("seq", 1L),
                new FindOneAndUpdateOptions<BsonDocument>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                });

            return result["seq"].ToInt64();
        }
    }
}
