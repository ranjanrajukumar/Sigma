using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class MClassTeacherRepository : IMClassTeacherRepository
    {
        private readonly DapperContext _context;

        public MClassTeacherRepository(DapperContext context)
        {
            _context = context;
        }

        private const string ClassTeacherColumns = @"
            class_teacher_id AS ClassTeacherId,
            academic_year_id AS AcademicYearId,
            school_id AS SchoolId,
            class_id AS ClassId,
            section_id AS SectionId,
            teacher_id AS TeacherId,
            is_active AS IsActive,
            auth_add AS AuthAdd,
            auth_lst_edt AS AuthLstEdt,
            auth_del AS AuthDel,
            add_on_dt AS AddOnDt,
            edit_on_dt AS EditOnDt,
            del_on_dt AS DelOnDt,
            del_status AS DelStatus";

        public async Task<IEnumerable<MClassTeacher>> GetAllAsync()
        {
            var sql = $@"
                SELECT {ClassTeacherColumns}
                FROM s_master.m_class_teacher
                WHERE del_status = false
                ORDER BY class_teacher_id";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MClassTeacher>(sql);
        }

        public async Task<MClassTeacher?> GetByIdAsync(long id)
        {
            var sql = $@"
                SELECT {ClassTeacherColumns}
                FROM s_master.m_class_teacher
                WHERE class_teacher_id = @Id
                AND del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MClassTeacher>(sql, new { Id = id });
        }

        public async Task<long> CreateAsync(MClassTeacher entity)
        {
            var sql = @"
                INSERT INTO s_master.m_class_teacher
                (
                    academic_year_id,
                    school_id,
                    class_id,
                    section_id,
                    teacher_id,
                    is_active,
                    auth_add
                )
                VALUES
                (
                    @AcademicYearId,
                    @SchoolId,
                    @ClassId,
                    @SectionId,
                    @TeacherId,
                    @IsActive,
                    @AuthAdd
                )
                RETURNING class_teacher_id";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(sql, entity);
        }

        public async Task<bool> UpdateAsync(MClassTeacher entity)
        {
            var sql = @"
                UPDATE s_master.m_class_teacher
                SET
                    academic_year_id = @AcademicYearId,
                    school_id = @SchoolId,
                    class_id = @ClassId,
                    section_id = @SectionId,
                    teacher_id = @TeacherId,
                    is_active = @IsActive,
                    auth_lst_edt = @AuthLstEdt,
                    edit_on_dt = CURRENT_TIMESTAMP
                WHERE class_teacher_id = @ClassTeacherId
                AND del_status = false";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(sql, entity);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id, string deletedBy)
        {
            var sql = @"
                UPDATE s_master.m_class_teacher
                SET
                    del_status = true,
                    auth_del = @DeletedBy,
                    del_on_dt = CURRENT_TIMESTAMP
                WHERE class_teacher_id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(sql, new { Id = id, DeletedBy = deletedBy });

            return rows > 0;
        }
    }
}