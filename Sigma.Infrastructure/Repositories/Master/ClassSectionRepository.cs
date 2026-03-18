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

        public async Task<ClassSection?> GetByIdAsync(long id)
        {
            string query = @"
            SELECT
            class_section_id AS ClassSectionId,
            class_id AS ClassId,
            section_id AS SectionId,
            room_number AS RoomNumber,
            floor AS Floor,
            section_capacity AS SectionCapacity,
            class_teacher_id AS ClassTeacherId,
            class_section_code AS ClassSectionCode,
            del_status AS DelStatus
            FROM s_master.m_class_section
            WHERE class_section_id = @Id
            AND del_status = false";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ClassSection>(query, new { Id = id });
        }

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
            AND del_status = false";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ClassSection>(query, new { ClassId = classId, SectionId = sectionId });
        }

        public async Task<long> AddAsync(ClassSection entity)
        {
            string query = @"
            INSERT INTO s_master.m_class_section
            (class_id, section_id, room_number, floor, section_capacity,
             class_teacher_id, class_section_code, auth_add, add_on_dt, del_status)
            VALUES
            (@ClassId,@SectionId,@RoomNumber,@Floor,@SectionCapacity,
             @ClassTeacherId,@ClassSectionCode,@AuthAdd,NOW(),false)
            RETURNING class_section_id";

            using var connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<long>(query, entity);
        }

        public async Task UpdateAsync(ClassSection entity)
        {
            string query = @"
            UPDATE s_master.m_class_section
            SET
            class_id=@ClassId,
            section_id=@SectionId,
            room_number=@RoomNumber,
            floor=@Floor,
            section_capacity=@SectionCapacity,
            class_teacher_id=@ClassTeacherId,
            auth_lst_edt=@AuthLstEdt,
            edit_on_dt=NOW()
            WHERE class_section_id=@ClassSectionId
            AND del_status=false";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, entity);
        }

        public async Task SoftDeleteAsync(ClassSection entity)
        {
            string query = @"
            UPDATE s_master.m_class_section
            SET
            del_status=true,
            auth_del=@AuthDel,
            del_on_dt=NOW()
            WHERE class_section_id=@ClassSectionId";

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
            s.section_name AS SectionName,
            cs.room_number AS RoomNumber,
            cs.floor AS Floor,
            cs.section_capacity AS SectionCapacity,
            cs.class_teacher_id AS ClassTeacherId,
            cs.class_section_code AS ClassSectionCode
            FROM s_master.m_class_section cs
            JOIN s_master.m_class c ON cs.class_id=c.class_id
            JOIN s_master.m_section_lookup s ON cs.section_id=s.section_id
            WHERE cs.del_status=false";

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
            s.section_name AS SectionName,
            cs.room_number AS RoomNumber,
            cs.floor AS Floor,
            cs.section_capacity AS SectionCapacity,
            cs.class_teacher_id AS ClassTeacherId,
            cs.class_section_code AS ClassSectionCode
            FROM s_master.m_class_section cs
            JOIN s_master.m_class c ON cs.class_id=c.class_id
            JOIN s_master.m_section_lookup s ON cs.section_id=s.section_id
            WHERE cs.class_section_id=@Id
            AND cs.del_status=false";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ClassSectionResponseDto>(query, new { Id = id });
        }
    }
}