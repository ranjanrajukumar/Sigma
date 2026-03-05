using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class AcademicYearRepository : IAcademicYearRepository
    {
        private readonly DapperContext _context;

        public AcademicYearRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AcademicYear>> GetAllAsync()
        {
            const string sql = @"
                SELECT 
                    academic_year_id AS AcademicYearId,
                    academic_year AS AcademicYearName,
                    start_date AS StartDate,
                    end_date AS EndDate,
                    is_active AS IsActive,
                    del_status AS DelStatus
                FROM s_master.m_academic_year
                WHERE del_status = false
                ORDER BY academic_year_id DESC;";

            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<AcademicYear>(sql);
        }

        public async Task<AcademicYear?> GetByIdAsync(long id)
        {
            const string sql = @"
                SELECT 
                    academic_year_id AS AcademicYearId,
                    academic_year AS AcademicYearName,
                    start_date AS StartDate,
                    end_date AS EndDate,
                    is_active AS IsActive,
                    del_status AS DelStatus
                FROM s_master.m_academic_year
                WHERE academic_year_id = @Id
                AND del_status = false;";

            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<AcademicYear>(sql, new { Id = id });
        }

        public async Task<bool> ExistsAsync(string academicYearName)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM s_master.m_academic_year
                WHERE academic_year = @AcademicYearName
                AND del_status = false;";

            using var conn = _context.CreateConnection();
            var count = await conn.ExecuteScalarAsync<int>(sql, new { AcademicYearName = academicYearName });
            return count > 0;
        }

        public async Task<long> CreateAsync(AcademicYear entity)
        {
            const string sql = @"
                INSERT INTO s_master.m_academic_year
                (academic_year, start_date, end_date, is_active, add_on_dt)
                VALUES (@AcademicYearName, @StartDate, @EndDate, @IsActive, NOW())
                RETURNING academic_year_id;";

            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<long>(sql, entity);
        }

        public async Task<bool> UpdateAsync(AcademicYear entity)
        {
            const string sql = @"
                UPDATE s_master.m_academic_year
                SET academic_year = @AcademicYearName,
                    start_date = @StartDate,
                    end_date = @EndDate,
                    is_active = @IsActive,
                    edit_on_dt = NOW()
                WHERE academic_year_id = @AcademicYearId;";

            using var conn = _context.CreateConnection();
            var rows = await conn.ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string sql = @"
                UPDATE s_master.m_academic_year
                SET del_status = true,
                    del_on_dt = NOW()
                WHERE academic_year_id = @Id;";

            using var conn = _context.CreateConnection();
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task InsertTermsAsync(List<AcademicYearTerm> terms)
        {
            const string sql = @"INSERT INTO s_master.m_academic_year_term
                            (academic_year_id,term_name,start_date,end_date,working_days,add_on_dt)
                            VALUES
                            (@AcademicYearId,@TermName,@StartDate,@EndDate,@WorkingDays,NOW())";

            using var conn = _context.CreateConnection();

            await conn.ExecuteAsync(sql, terms);
        }

        public async Task<AcademicYear?> GetAcademicYearWithTerms(long id)
        {
            const string sql = @"
    SELECT 
        ay.academic_year_id AS AcademicYearId,
        ay.academic_year AS AcademicYearName,
        ay.start_date AS StartDate,
        ay.end_date AS EndDate,
        ay.is_active AS IsActive,

        t.term_id AS TermId,
        t.term_name AS TermName,
        t.start_date AS StartDate,
        t.end_date AS EndDate,
        t.working_days AS WorkingDays

    FROM s_master.m_academic_year ay
    LEFT JOIN s_master.m_academic_year_term t
        ON ay.academic_year_id = t.academic_year_id
    WHERE ay.academic_year_id = @Id
    AND ay.del_status = false";

            using var conn = _context.CreateConnection();

            var lookup = new Dictionary<long, AcademicYear>();

            var result = await conn.QueryAsync<AcademicYear, AcademicYearTerm, AcademicYear>(
                sql,
                (year, term) =>
                {
                    if (!lookup.TryGetValue(year.AcademicYearId, out var academicYear))
                    {
                        academicYear = year;
                        academicYear.Terms = new List<AcademicYearTerm>();
                        lookup.Add(academicYear.AcademicYearId, academicYear);
                    }

                    if (term != null)
                        academicYear.Terms.Add(term);

                    return academicYear;
                },
                new { Id = id },
                splitOn: "TermId"
            );

            return lookup.Values.FirstOrDefault();
        }
        public async Task UpdateTermAsync(AcademicYearTerm term)
        {
            const string sql = @"UPDATE s_master.m_academic_year_term
                         SET term_name=@TermName,
                             start_date=@StartDate,
                             end_date=@EndDate,
                             working_days=@WorkingDays,
                             edit_on_dt=NOW()
                         WHERE term_id=@TermId";

            using var conn = _context.CreateConnection();

            await conn.ExecuteAsync(sql, term);
        }

        public async Task InsertTermAsync(AcademicYearTerm term)
        {
            const string sql = @"INSERT INTO s_master.m_academic_year_term
                        (academic_year_id,term_name,start_date,end_date,working_days,add_on_dt)
                        VALUES
                        (@AcademicYearId,@TermName,@StartDate,@EndDate,@WorkingDays,NOW())";

            using var conn = _context.CreateConnection();

            await conn.ExecuteAsync(sql, term);
        }

        public async Task<IEnumerable<AcademicYear>> GetAllWithTermsAsync()
        {
            const string sql = @"
    SELECT 
        ay.academic_year_id AS AcademicYearId,
        ay.academic_year AS AcademicYearName,
        ay.start_date AS StartDate,
        ay.end_date AS EndDate,
        ay.is_active AS IsActive,

        t.term_id AS TermId,
        t.term_name AS TermName,
        t.start_date AS StartDate,
        t.end_date AS EndDate,
        t.working_days AS WorkingDays

    FROM s_master.m_academic_year ay
    LEFT JOIN s_master.m_academic_year_term t
        ON ay.academic_year_id = t.academic_year_id
    WHERE ay.del_status = false
    ORDER BY ay.academic_year_id DESC";

            using var conn = _context.CreateConnection();

            var dictionary = new Dictionary<long, AcademicYear>();

            var result = await conn.QueryAsync<AcademicYear, AcademicYearTerm, AcademicYear>(
                sql,
                (year, term) =>
                {
                    if (!dictionary.TryGetValue(year.AcademicYearId, out var academicYear))
                    {
                        academicYear = year;
                        academicYear.Terms = new List<AcademicYearTerm>();
                        dictionary.Add(academicYear.AcademicYearId, academicYear);
                    }

                    if (term != null)
                        academicYear.Terms.Add(term);

                    return academicYear;
                },
                splitOn: "TermId"
            );

            return dictionary.Values;
        }

    }
}
