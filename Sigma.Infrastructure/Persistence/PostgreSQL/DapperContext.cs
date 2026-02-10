using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sigma.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            // Prefer an explicit environment variable so secrets can be injected
            // at runtime (containers, CI, secret stores). Fall back to
            // ConnectionStrings:Postgres from configuration.
            var envConn = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");
            var cfgConn = _configuration.GetConnectionString("Postgres");

            var connectionString = !string.IsNullOrWhiteSpace(envConn)
                ? envConn
                : cfgConn;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "Postgres connection string not configured. Set ConnectionStrings:Postgres or POSTGRES_CONNECTION.");

            return new NpgsqlConnection(connectionString);
        }
    }
}
