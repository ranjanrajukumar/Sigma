using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly DapperContext _context;

        public EventTypeRepository(DapperContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<long> CreateEventType(EventType eventType)
        {
            using var connection = _context.CreateConnection();

            // Check duplicate
            var checkQuery = @"SELECT COUNT(1)
                               FROM s_master.m_event_type
                               WHERE LOWER(event_type_name) = LOWER(@EventTypeName)
                               AND del_status = FALSE";

            var exists = await connection.ExecuteScalarAsync<int>(checkQuery, new
            {
                eventType.EventTypeName
            });

            if (exists > 0)
                return -1; // Duplicate

            var insertQuery = @"INSERT INTO s_master.m_event_type
                                (event_type_name, description)
                                VALUES
                                (@EventTypeName, @Description)
                                RETURNING event_type_id";

            return await connection.ExecuteScalarAsync<long>(insertQuery, eventType);
        }

        // GET ALL
        public async Task<IEnumerable<EventType>> GetAllEventTypes()
        {
            var query = @"SELECT
                            event_type_id AS EventTypeId,
                            event_type_name AS EventTypeName,
                            description AS Description,
                            is_active AS IsActive
                          FROM s_master.m_event_type
                          WHERE del_status = FALSE
                          ORDER BY event_type_name";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<EventType>(query);
        }

        // GET BY ID
        public async Task<EventType> GetEventTypeById(long id)
        {
            var query = @"SELECT
                            event_type_id AS EventTypeId,
                            event_type_name AS EventTypeName,
                            description AS Description,
                            is_active AS IsActive
                          FROM s_master.m_event_type
                          WHERE event_type_id = @Id
                          AND del_status = FALSE";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<EventType>(query, new { Id = id });
        }

        // UPDATE
        public async Task<bool> UpdateEventType(EventType eventType)
        {
            using var connection = _context.CreateConnection();

            // Duplicate check excluding current record
            var checkQuery = @"SELECT COUNT(1)
                               FROM s_master.m_event_type
                               WHERE LOWER(event_type_name) = LOWER(@EventTypeName)
                               AND event_type_id <> @EventTypeId
                               AND del_status = FALSE";

            var exists = await connection.ExecuteScalarAsync<int>(checkQuery, eventType);

            if (exists > 0)
                return false;

            var updateQuery = @"UPDATE s_master.m_event_type
                                SET
                                event_type_name = @EventTypeName,
                                description = @Description,
                                is_active = @IsActive,
                                edit_on_dt = CURRENT_TIMESTAMP
                                WHERE event_type_id = @EventTypeId
                                AND del_status = FALSE";

            var result = await connection.ExecuteAsync(updateQuery, eventType);

            return result > 0;
        }

        // DELETE (SOFT DELETE)
        public async Task<bool> DeleteEventType(long id)
        {
            var query = @"UPDATE s_master.m_event_type
                          SET
                          del_status = TRUE,
                          del_on_dt = CURRENT_TIMESTAMP
                          WHERE event_type_id = @Id
                          AND del_status = FALSE";

            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(query, new { Id = id });

            return result > 0;
        }
    }
}