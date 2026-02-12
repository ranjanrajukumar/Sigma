using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class MClassRepository : IMClassRepository
    {
        private readonly DapperContext _context;

        public MClassRepository(DapperContext context)
        {
            _context = context;
        }

        // =========================================
        // GET ALL
        // =========================================
        public async Task<IEnumerable<MClass>> GetAllAsync()
        {
            const string sql = @"
                SELECT
                    class_id AS ClassId,
                    class_name AS ClassName,
                    class_order AS ClassOrder,
                    
                    COALESCE(del_status, false) AS DelStatus
                FROM s_master.m_class
                WHERE COALESCE(del_status, false) = false
                ORDER BY class_order;
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MClass>(sql);
        }

        // =========================================
        // GET BY ID
        // =========================================
        public async Task<MClass?> GetByIdAsync(long id)
        {
            const string sql = @"
                SELECT
                    class_id AS ClassId,
                    class_name AS ClassName,
                    class_order AS ClassOrder,
                    
                    COALESCE(del_status, false) AS DelStatus
                FROM s_master.m_class
                WHERE class_id = @Id
                AND COALESCE(del_status, false) = false;
            ";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MClass>(sql, new { Id = id });
        }

        // =========================================
        // CREATE
        // =========================================
        public async Task<long> CreateAsync(MClass entity)
        {
            const string sql = @"
                INSERT INTO s_master.m_class
                (class_name, class_order, add_on_dt)
                VALUES (@ClassName, @ClassOrder, NOW())
                RETURNING class_id;
            ";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(sql, entity);
        }

        // =========================================
        // UPDATE
        // =========================================
        public async Task<bool> UpdateAsync(MClass entity)
        {
            const string sql = @"
                UPDATE s_master.m_class
                SET class_name = @ClassName,
                    class_order = @ClassOrder,
                    
                    edit_on_dt = NOW()
                WHERE class_id = @ClassId
                AND COALESCE(del_status, false) = false;
            ";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(sql, entity);
            return rows > 0;
        }

        // =========================================
        // SOFT DELETE
        // =========================================
        public async Task<bool> DeleteAsync(long id)
        {
            const string sql = @"
                UPDATE s_master.m_class
                SET del_status = true,
                    del_on_dt = NOW()
                WHERE class_id = @Id;
            ";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
