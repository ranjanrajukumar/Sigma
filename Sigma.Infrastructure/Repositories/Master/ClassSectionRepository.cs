using Dapper;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class ClassSectionRepository : IClassSectionRepository
    {
        private readonly DapperContext _context;

        public ClassSectionRepository(DapperContext context)
        {
            _context = context;
        }

        // ✅ 1️⃣ Get All Active
        public async Task<IEnumerable<ClassSection>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    class_section_id AS ClassSectionId,
                    class_id AS ClassId,
                    section_id AS SectionId,
                    auth_add AS AuthAdd,
                    auth_lst_edt AS AuthLstEdt,
                    auth_del AS AuthDel,
                    add_on_dt AS AddOnDt,
                    edit_on_dt AS EditOnDt,
                    del_on_dt AS DelOnDt,
                    del_status AS DelStatus
                FROM s_master.m_class_section
                WHERE del_status = false
                ORDER BY class_section_id;";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ClassSection>(query);
        }

        // ✅ 2️⃣ Get By Id
        public async Task<ClassSection?> GetByIdAsync(long id)
        {
            string query = @"
                SELECT 
                    class_section_id AS ClassSectionId,
                    class_id AS ClassId,
                    section_id AS SectionId,
                    auth_add AS AuthAdd,
                    auth_lst_edt AS AuthLstEdt,
                    auth_del AS AuthDel,
                    add_on_dt AS AddOnDt,
                    edit_on_dt AS EditOnDt,
                    del_on_dt AS DelOnDt,
                    del_status AS DelStatus
                FROM s_master.m_class_section
                WHERE class_section_id = @Id
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ClassSection>(query, new { Id = id });
        }

        // ✅ 3️⃣ Get By Class & Section (Unique Validation)
        public async Task<ClassSection?> GetByClassAndSectionAsync(long classId, long sectionId)
        {
            string query = @"
                SELECT 
                    class_section_id AS ClassSectionId,
                    class_id AS ClassId,
                    section_id AS SectionId,
                    del_status AS DelStatus
                FROM s_master.m_class_section
                WHERE class_id = @ClassId
                AND section_id = @SectionId
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ClassSection>(
                query, new { ClassId = classId, SectionId = sectionId });
        }

        // ✅ 4️⃣ Insert
        public async Task<long> AddAsync(ClassSection entity)
        {
            string query = @"
                INSERT INTO s_master.m_class_section
                (class_id, section_id, auth_add, add_on_dt, del_status)
                VALUES (@ClassId, @SectionId, @AuthAdd, NOW(), false)
                RETURNING class_section_id;";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(query, entity);
        }

        // ✅ 5️⃣ Update
        public async Task UpdateAsync(ClassSection entity)
        {
            string query = @"
                UPDATE s_master.m_class_section
                SET class_id = @ClassId,
                    section_id = @SectionId,
                    auth_lst_edt = @AuthLstEdt,
                    edit_on_dt = NOW()
                WHERE class_section_id = @ClassSectionId
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        // ✅ 6️⃣ Soft Delete
        public async Task SoftDeleteAsync(ClassSection entity)
        {
            string query = @"
                UPDATE s_master.m_class_section
                SET del_status = true,
                    auth_del = @AuthDel,
                    del_on_dt = NOW()
                WHERE class_section_id = @ClassSectionId
                AND del_status = false;";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetAllWithNamesAsync()
        {
            string query = @"
        SELECT 
            cs.class_section_id AS ClassSectionId,
            cs.class_id AS ClassId,
            c.class_name AS ClassName,
            cs.section_id AS SectionId,
            s.section_name AS SectionName
        FROM s_master.m_class_section cs
        JOIN s_master.m_class c 
            ON cs.class_id = c.class_id
        JOIN s_master.m_section_lookup s 
            ON cs.section_id = s.section_id
        WHERE cs.del_status = false
        ORDER BY cs.class_section_id;";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<ClassSectionResponseDto>(query);
        }

        public async Task<ClassSectionResponseDto?> GetByIdWithNamesAsync(long id)
        {
            string query = @"
        SELECT 
            cs.class_section_id AS ClassSectionId,
            cs.class_id AS ClassId,
            c.class_name AS ClassName,
            cs.section_id AS SectionId,
            s.section_name AS SectionName
        FROM s_master.m_class_section cs
        JOIN s_master.m_class c 
            ON cs.class_id = c.class_id
        JOIN s_master.m_section_lookup s 
            ON cs.section_id = s.section_id
        WHERE cs.class_section_id = @Id
        AND cs.del_status = false;";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ClassSectionResponseDto>(
                query,
                new { Id = id });
        }

    }
}
