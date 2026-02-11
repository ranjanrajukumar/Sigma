using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sigma.Domain.Entities.Utilities;

namespace Sigma.Infrastructure.Persistence
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        private const string LogCollectionName = "global_activity_logs";
        private const string CounterCollectionName = "counters";

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoSettings:ConnectionString"];
            var databaseName = configuration["MongoSettings:DatabaseName"];

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("MongoSettings:ConnectionString is missing");

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new InvalidOperationException("MongoSettings:DatabaseName is missing");

            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }

        // 🔹 Called at application startup
        public async Task InitializeAsync()
        {
            await EnsureCollectionsAsync();
            await EnsureIndexesAsync();
        }

        private async Task EnsureCollectionsAsync()
        {
            var collectionNames = await Database
                .ListCollectionNames()
                .ToListAsync();

            if (!collectionNames.Contains(LogCollectionName))
            {
                await Database.CreateCollectionAsync(LogCollectionName);
            }

            if (!collectionNames.Contains(CounterCollectionName))
            {
                await Database.CreateCollectionAsync(CounterCollectionName);
            }
        }

        //private async Task EnsureIndexesAsync()
        //{
        //    var collection = Database
        //        .GetCollection<GlobalActivityLog>(LogCollectionName);

        //    var existingIndexes = await collection
        //        .Indexes
        //        .ListAsync();

        //    var indexList = await existingIndexes.ToListAsync();
        //    var indexNames = indexList
        //        .Select(i => i["name"].AsString)
        //        .ToList();

        //    var models = new List<CreateIndexModel<GlobalActivityLog>>();

        //    // Unique LogNo
        //    if (!indexNames.Contains("LogNo_1"))
        //    {
        //        models.Add(new CreateIndexModel<GlobalActivityLog>(
        //            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.LogNo),
        //            new CreateIndexOptions { Unique = true, Name = "LogNo_1" }));
        //    }

        //    // Level index
        //    if (!indexNames.Contains("Level_1"))
        //    {
        //        models.Add(new CreateIndexModel<GlobalActivityLog>(
        //            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.Level),
        //            new CreateIndexOptions { Name = "Level_1" }));
        //    }

        //    // CreatedAt descending index
        //    if (!indexNames.Contains("CreatedAt_-1"))
        //    {
        //        models.Add(new CreateIndexModel<GlobalActivityLog>(
        //            Builders<GlobalActivityLog>.IndexKeys.Descending(x => x.CreatedAt),
        //            new CreateIndexOptions { Name = "CreatedAt_-1" }));
        //    }

        //    // TTL index (30 days)
        //    if (!indexNames.Contains("CreatedAt_TTL"))
        //    {
        //        models.Add(new CreateIndexModel<GlobalActivityLog>(
        //            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.CreatedAt),
        //            new CreateIndexOptions
        //            {
        //                ExpireAfter = TimeSpan.FromDays(30),
        //                Name = "CreatedAt_TTL"
        //            }));
        //    }

        //    if (models.Count > 0)
        //    {
        //        await collection.Indexes.CreateManyAsync(models);
        //    }
        //}


        private async Task EnsureIndexesAsync()
        {
            var collection = Database.GetCollection<GlobalActivityLog>(LogCollectionName);

            var models = new List<CreateIndexModel<GlobalActivityLog>>
    {
        new CreateIndexModel<GlobalActivityLog>(
            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.LogNo),
            new CreateIndexOptions { Unique = true }),

        new CreateIndexModel<GlobalActivityLog>(
            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.Level)),

        new CreateIndexModel<GlobalActivityLog>(
            Builders<GlobalActivityLog>.IndexKeys.Descending(x => x.CreatedAt)),

        new CreateIndexModel<GlobalActivityLog>(
            Builders<GlobalActivityLog>.IndexKeys.Ascending(x => x.CreatedAt),
            new CreateIndexOptions
            {
                ExpireAfter = TimeSpan.FromDays(30)
            })
    };

            try
            {
                await collection.Indexes.CreateManyAsync(models);
            }
            catch (MongoCommandException)
            {
                // Index already exists → ignore safely
            }
        }

    }
}
