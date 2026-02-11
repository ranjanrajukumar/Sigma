using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Infrastructure.Persistence
{

    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

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
    }
}
