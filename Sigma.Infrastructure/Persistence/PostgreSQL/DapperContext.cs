using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Sigma.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            var envConn = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");
            var cfgConn = configuration.GetConnectionString("Postgres");

            var rawConnection = !string.IsNullOrWhiteSpace(envConn)
                ? envConn
                : cfgConn;

            if (string.IsNullOrWhiteSpace(rawConnection))
                throw new InvalidOperationException(
                    "Postgres connection string not configured. Set ConnectionStrings:Postgres or POSTGRES_CONNECTION.");

            // 🔥 Force safe pooling settings
            var builder = new NpgsqlConnectionStringBuilder(rawConnection)
            {
                Pooling = true,          // Enable pooling
                MinPoolSize = 0,
                MaxPoolSize = 10,        // 🔥 IMPORTANT: Reduce if using free hosting
                Timeout = 15,
                CommandTimeout = 30
            };

            _connectionString = builder.ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
