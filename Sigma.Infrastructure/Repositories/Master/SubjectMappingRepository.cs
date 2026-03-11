using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;
using System.Data;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class SubjectMappingRepository : ISubjectMappingRepository
    {
        private readonly DapperContext _context;

        public SubjectMappingRepository(DapperContext context)
        {
            _context = context;
        }

        private const string SubjectMappingColumns = @"
            subject_mapping_id AS SubjectMappingId,
            academic_year_id AS AcademicYearId,
            school_id AS SchoolId,
            class_id AS ClassId,
            section_id AS SectionId,
            is_all_sections AS IsAllSections,
            term_id AS TermId,
            subject_id AS SubjectId,
            periods_per_week AS PeriodsPerWeek,
            subject_type AS SubjectType,
            is_active AS IsActive,
            auth_add AS AuthAdd,
            auth_lst_edt AS AuthLstEdt,
            auth_del AS AuthDel,
            add_on_dt AS AddOnDt,
            edit_on_dt AS EditOnDt,
            del_on_dt AS DelOnDt,
            del_status AS DelStatus
        ";

        public async Task<IEnumerable<SubjectMapping>> GetAllAsync()
        {
            var sql = $@"
                SELECT {SubjectMappingColumns}
                FROM s_master.m_subject_mapping
                WHERE del_status = false
                ORDER BY subject_mapping_id";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<SubjectMapping>(sql);
        }

        public async Task<SubjectMapping?> GetByIdAsync(long id)
        {
            var sql = $@"
                SELECT {SubjectMappingColumns}
                FROM s_master.m_subject_mapping
                WHERE subject_mapping_id = @Id
                AND del_status = false";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<SubjectMapping>(sql, new { Id = id });
        }

        public async Task<long> CreateAsync(SubjectMapping entity)
        {
            var sql = @"
                INSERT INTO s_master.m_subject_mapping
                (
                    academic_year_id,
                    school_id,
                    class_id,
                    section_id,
                    is_all_sections,
                    term_id,
                    subject_id,
                    periods_per_week,
                    subject_type,
                    auth_add
                )
                VALUES
                (
                    @AcademicYearId,
                    @SchoolId,
                    @ClassId,
                    @SectionId,
                    @IsAllSections,
                    @TermId,
                    @SubjectId,
                    @PeriodsPerWeek,
                    @SubjectType,
                    @AuthAdd
                )
                RETURNING subject_mapping_id";

            using var connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<long>(sql, entity);
        }

        public async Task<bool> UpdateAsync(SubjectMapping entity)
        {
            var sql = @"
                UPDATE s_master.m_subject_mapping
                SET
                    academic_year_id = @AcademicYearId,
                    school_id = @SchoolId,
                    class_id = @ClassId,
                    section_id = @SectionId,
                    is_all_sections = @IsAllSections,
                    term_id = @TermId,
                    subject_id = @SubjectId,
                    periods_per_week = @PeriodsPerWeek,
                    subject_type = @SubjectType,
                    auth_lst_edt = @AuthLstEdt,
                    edit_on_dt = CURRENT_TIMESTAMP
                WHERE subject_mapping_id = @SubjectMappingId";

            using var connection = _context.CreateConnection();

            var rows = await connection.ExecuteAsync(sql, entity);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id, string deletedBy)
        {
            var sql = @"
                UPDATE s_master.m_subject_mapping
                SET
                    del_status = true,
                    auth_del = @DeletedBy,
                    del_on_dt = CURRENT_TIMESTAMP
                WHERE subject_mapping_id = @Id";

            using var connection = _context.CreateConnection();

            var rows = await connection.ExecuteAsync(sql, new { Id = id, DeletedBy = deletedBy });

            return rows > 0;
        }
    }
}