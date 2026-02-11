using Dapper;
using Sigma.Application.DTOs.Common;
using Sigma.Application.Interfaces.Common;
using Sigma.Infrastructure.Persistence;
using System.Data;
using System.Text.Json;
using System.Threading;

namespace Sigma.Infrastructure.Repositories.Common
{
    public class CommonSearchRepository : ICommonSearchRepository
    {
        // Limit concurrent repository DB calls to avoid exhausting Postgres connections.
        // Adjust the initial count to match your DB capacity / app throughput needs.
        private static readonly SemaphoreSlim _connectionGate = new SemaphoreSlim(50, 100);

        private readonly DapperContext _context;

        public CommonSearchRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<CommonSearchResponseDto> SearchAsync(
            string schemaName,
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string? otherCondition,
            string? sortBy)
        {                                                                                                                                           
            // Throttle concurrent DB access so we don't exceed PostgreSQL role connection limits.
            await _connectionGate.WaitAsync();
            try
            {
                await using var conn = (Npgsql.NpgsqlConnection)_context.CreateConnection();

                // Ensure the connection is open before executing the query.
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                var parameters = new
                {
                    p_schema_name = schemaName,
                    p_table_name = tableName,
                    p_column_id = columnId,
                    p_display_columns = displayColumns,
                    p_search_term = searchTerm,
                    p_other_condition = otherCondition,
                    p_sort_by = sortBy
                };

                var query = @"SELECT * FROM search.usp_common_search(
                                @p_schema_name,
                                @p_table_name,
                                @p_column_id,
                                @p_display_columns,
                                @p_search_term,
                                @p_other_condition,
                                @p_sort_by)";

                var result = await conn.QueryAsync(query, parameters);

                var response = new CommonSearchResponseDto
                {
                    DisplayName = displayName,
                    Headers = displayColumns.Split(',')
                                            .Select(x => x.Trim())
                                            .ToList()
                };

                foreach (var row in result)
                {
                    var dict = new Dictionary<string, string>();

                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(
                        row.result?.ToString() ?? "{}");

                    foreach (var header in response.Headers)
                    {
                        dict[header] = jsonData != null && jsonData.ContainsKey(header)
                            ? jsonData[header]?.ToString() ?? ""
                            : "";
                    }

                    response.Data.Add(new CommonSearchRowDto
                    {
                        Id = Convert.ToInt32(row.id),
                        Columns = dict
                    });
                }

                return response;
            }
            finally
            {
                _connectionGate.Release();
            }
        }
    }
}