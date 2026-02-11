using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            var filter = Builders<BsonDocument>.Filter.Eq("_id", key);
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);

            var result = await _counter.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<BsonDocument>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                });

            return result["seq"].AsInt64;
        }
    }
}
