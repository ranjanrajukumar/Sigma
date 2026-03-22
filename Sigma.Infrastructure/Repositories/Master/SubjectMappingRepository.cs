using Dapper;
using Sigma.Application.DTOs.Master;
using Sigma.Application.DTOs.Master.Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class SubjectMappingRepository : ISubjectMappingRepository
    {
        private readonly DapperContext _context;

        public SubjectMappingRepository(DapperContext context)
        {
            _context = context;
        }

        private readonly string BaseQuery = @"
            SELECT  
                sm.subject_mapping_id AS SubjectMappingId,

                sm.academic_year_id AS AcademicYearId,
                ay.academic_year AS AcademicYearName,

                sm.school_id AS SchoolId,
                sc.school_name AS SchoolName,

                sm.class_id AS ClassId,
                c.class_name AS ClassName,

                sm.section_id AS SectionId,
                sec.section_name AS SectionName,

                sm.is_all_sections AS IsAllSections,

                sm.term_id AS TermId,
                t.term_name AS TermName,

                sm.subject_id AS SubjectId,
                sub.subject_name AS SubjectName,

                sm.periods_per_week AS PeriodsPerWeek,
                sm.subject_type AS SubjectType,
                sm.is_active AS IsActive,

                sm.auth_add AS AuthAdd,
                sm.auth_lst_edt AS AuthLstEdt,
                sm.auth_del AS AuthDel,
                sm.add_on_dt AS AddOnDt,
                sm.edit_on_dt AS EditOnDt,
                sm.del_on_dt AS DelOnDt,
                sm.del_status AS DelStatus

            FROM s_master.m_subject_mapping sm

            LEFT JOIN s_master.m_academic_year ay 
                ON sm.academic_year_id = ay.academic_year_id

            LEFT JOIN s_master.m_school sc 
                ON sm.school_id = sc.school_id

            LEFT JOIN s_master.m_class c 
                ON sm.class_id = c.class_id

            LEFT JOIN s_master.m_section_lookup sec 
                ON sm.section_id = sec.section_id

            LEFT JOIN s_master.m_subject sub 
                ON sm.subject_id = sub.subject_id

            LEFT JOIN s_master.m_academic_year_term t 
                ON sm.term_id = t.term_id

            WHERE sm.del_status = false
        ";

        public async Task<IEnumerable<SubjectMappingResponseDto>> GetAllAsync()
        {
            var sql = BaseQuery + " ORDER BY sm.subject_mapping_id DESC";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<SubjectMappingResponseDto>(sql);
        }

        public async Task<SubjectMappingResponseDto?> GetByIdAsync(long id)
        {
            var sql = BaseQuery + " AND sm.subject_mapping_id = @Id";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SubjectMappingResponseDto>(sql, new { Id = id });
        }

        public async Task<long> CreateAsync(SubjectMappingCreateDto dto)
        {
            using var connection = _context.CreateConnection();

            var checkSql = @"
                SELECT 1 FROM s_master.m_subject_mapping
                WHERE academic_year_id = @AcademicYearId
                AND class_id = @ClassId
                AND section_id IS NOT DISTINCT FROM @SectionId
                AND subject_id = @SubjectId
                AND term_id = @TermId
                AND del_status = false";

            var exists = await connection.ExecuteScalarAsync<int?>(checkSql, dto);

            if (exists != null)
                throw new Exception("Duplicate subject mapping not allowed");

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

            return await connection.ExecuteScalarAsync<long>(sql, dto);
        }

        public async Task<bool> UpdateAsync(long id, SubjectMappingCreateDto dto)
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
                    auth_lst_edt = @AuthAdd,
                    edit_on_dt = CURRENT_TIMESTAMP
                WHERE subject_mapping_id = @Id";

            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters(dto);
            parameters.Add("Id", id);

            var rows = await connection.ExecuteAsync(sql, parameters);
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