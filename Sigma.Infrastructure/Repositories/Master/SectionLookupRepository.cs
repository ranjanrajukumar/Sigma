using Dapper;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;
using Sigma.Infrastructure.Repositories.Interfaces;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class SectionLookupRepository : ISectionLookupRepository
    {
        private readonly DapperContext _context;

        public SectionLookupRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SectionLookup>> GetAllAsync()
        {
            string query = @"
                SELECT
                    section_id AS SectionId,
                    section_name AS SectionName,
                    section_code AS SectionCode,
                    auth_add AS AuthAdd,
                    auth_lst_edt AS AuthLstEdt,
                    auth_del AS AuthDel,
                    add_on_dt AS AddOnDt,
                    edit_on_dt AS EditOnDt,
                    del_on_dt AS DelOnDt,
                    del_status AS DelStatus
                FROM s_master.m_section_lookup
                WHERE del_status = false
                ORDER BY section_id;";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<SectionLookup>(query);
        }

        public async Task<SectionLookup?> GetByIdAsync(long id)
        {
            string query = @"
                SELECT
                    section_id AS SectionId,
                    section_name AS SectionName,
                    section_code AS SectionCode,
                    del_status AS DelStatus
                FROM s_master.m_section_lookup
                WHERE section_id = @Id
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SectionLookup>(query, new { Id = id });
        }

        public async Task<SectionLookup?> GetByNameAsync(string sectionName)
        {
            string query = @"
                SELECT
                    section_id AS SectionId,
                    section_name AS SectionName,
                    section_code AS SectionCode,
                    del_status AS DelStatus
                FROM s_master.m_section_lookup
                WHERE LOWER(section_name) = LOWER(@SectionName)
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SectionLookup>(query, new { SectionName = sectionName });
        }

        public async Task<long> AddAsync(SectionLookup entity)
        {
            string query = @"
                INSERT INTO s_master.m_section_lookup
                (section_name, section_code, auth_add, add_on_dt, del_status)
                VALUES (@SectionName, @SectionCode, @AuthAdd, NOW(), false)
                RETURNING section_id;";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(query, entity);
        }

        public async Task UpdateAsync(SectionLookup entity)
        {
            string query = @"
                UPDATE s_master.m_section_lookup
                SET
                    section_name = @SectionName,
                    section_code = @SectionCode,
                    auth_lst_edt = @AuthLstEdt,
                    edit_on_dt = NOW()
                WHERE section_id = @SectionId
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        public async Task SoftDeleteAsync(SectionLookup entity)
        {
            string query = @"
                UPDATE s_master.m_section_lookup
                SET
                    del_status = true,
                    auth_del = @AuthDel,
                    del_on_dt = NOW()
                WHERE section_id = @SectionId
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }
    }
}