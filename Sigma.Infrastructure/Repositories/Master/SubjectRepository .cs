using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;
using System.Data;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DapperContext _context;

        public SubjectRepository(DapperContext context)
        {
            _context = context;
        }

        private const string SubjectColumns = @"
            subject_id AS SubjectId,
            subject_name AS SubjectName,
            subject_code AS SubjectCode,
            is_optional AS IsOptional,
            subject_type AS SubjectType,
            min_marks AS MinMarks,
            max_marks AS MaxMarks,
            pass_marks AS PassMarks,
            auth_add AS AuthAdd,
            auth_lst_edt AS AuthLstEdt,
            auth_del AS AuthDel,
            add_on_dt AS AddOnDt,
            edit_on_dt AS EditOnDt,
            del_on_dt AS DelOnDt,
            del_status AS DelStatus";

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            string query = $@"
                SELECT {SubjectColumns}
                FROM s_master.m_subject
                WHERE del_status = false
                ORDER BY subject_name";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Subject>(query);
        }

        public async Task<Subject?> GetSubjectById(long id)
        {
            string query = $@"
                SELECT {SubjectColumns}
                FROM s_master.m_subject
                WHERE subject_id = @Id
                AND del_status = false";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Subject>(query, new { Id = id });
        }

        public async Task<long> CreateSubject(Subject subject)
        {
            string query = @"
                INSERT INTO s_master.m_subject
                (
                    subject_name,
                    subject_code,
                    is_optional,
                    subject_type,
                    min_marks,
                    max_marks,
                    pass_marks,
                    auth_add,
                    add_on_dt
                )
                VALUES
                (
                    @SubjectName,
                    @SubjectCode,
                    @IsOptional,
                    @SubjectType,
                    @MinMarks,
                    @MaxMarks,
                    @PassMarks,
                    @AuthAdd,
                    NOW()
                )
                RETURNING subject_id";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<long>(query, subject);
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            string query = @"
                UPDATE s_master.m_subject
                SET subject_name = @SubjectName,
                    subject_code = @SubjectCode,
                    is_optional = @IsOptional,
                    subject_type = @SubjectType,
                    min_marks = @MinMarks,
                    max_marks = @MaxMarks,
                    pass_marks = @PassMarks,
                    auth_lst_edt = @AuthLstEdt,
                    edit_on_dt = NOW()
                WHERE subject_id = @SubjectId
                AND del_status = false";

            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, subject);

            return result > 0;
        }

        public async Task<bool> DeleteSubject(long id)
        {
            string query = @"
                UPDATE s_master.m_subject
                SET del_status = true,
                    del_on_dt = NOW()
                WHERE subject_id = @Id";

            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(query, new { Id = id });

            return result > 0;
        }
    }
}