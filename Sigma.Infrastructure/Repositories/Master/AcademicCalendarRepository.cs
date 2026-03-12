using Dapper;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;
using Sigma.Infrastructure.Persistence;

namespace Sigma.Infrastructure.Repositories.Master
{
    public class AcademicCalendarRepository : IAcademicCalendarRepository
    {
        private readonly DapperContext _context;

        public AcademicCalendarRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<long> CreateAcademicCalendar(AcademicCalendar calendar)
        {
            var query = @"INSERT INTO s_master.m_academic_calendar
                        (
                            academic_year_id,
                            school_id,
                            class_id,
                            is_all_classes,
                            event_type_id,
                            event_title,
                            event_description,
                            start_date,
                            end_date,
                            is_holiday
                        )
                        VALUES
                        (
                            @AcademicYearId,
                            @SchoolId,
                            @ClassId,
                            @IsAllClasses,
                            @EventTypeId,
                            @EventTitle,
                            @EventDescription,
                            @StartDate,
                            @EndDate,
                            @IsHoliday
                        )
                        RETURNING academic_calendar_id";

            using var connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<long>(query, calendar);
        }

        public async Task<IEnumerable<AcademicCalendar>> GetAllAcademicCalendars()
        {
            var query = @"SELECT
                        academic_calendar_id AS AcademicCalendarId,
                        academic_year_id AS AcademicYearId,
                        school_id AS SchoolId,
                        class_id AS ClassId,
                        is_all_classes AS IsAllClasses,
                        event_type_id AS EventTypeId,
                        event_title AS EventTitle,
                        event_description AS EventDescription,
                        start_date AS StartDate,
                        end_date AS EndDate,
                        is_holiday AS IsHoliday
                        FROM s_master.m_academic_calendar
                        WHERE del_status = FALSE";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<AcademicCalendar>(query);
        }

        public async Task<AcademicCalendar> GetAcademicCalendarById(long id)
        {
            var query = @"SELECT
                        academic_calendar_id AS AcademicCalendarId,
                        academic_year_id AS AcademicYearId,
                        school_id AS SchoolId,
                        class_id AS ClassId,
                        is_all_classes AS IsAllClasses,
                        event_type_id AS EventTypeId,
                        event_title AS EventTitle,
                        event_description AS EventDescription,
                        start_date AS StartDate,
                        end_date AS EndDate,
                        is_holiday AS IsHoliday
                        FROM s_master.m_academic_calendar
                        WHERE academic_calendar_id = @Id
                        AND del_status = FALSE";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<AcademicCalendar>(query, new { Id = id });
        }

        public async Task<bool> UpdateAcademicCalendar(AcademicCalendar calendar)
        {
            var query = @"UPDATE s_master.m_academic_calendar
                        SET
                        academic_year_id = @AcademicYearId,
                        school_id = @SchoolId,
                        class_id = @ClassId,
                        is_all_classes = @IsAllClasses,
                        event_type_id = @EventTypeId,
                        event_title = @EventTitle,
                        event_description = @EventDescription,
                        start_date = @StartDate,
                        end_date = @EndDate,
                        is_holiday = @IsHoliday,
                        edit_on_dt = CURRENT_TIMESTAMP
                        WHERE academic_calendar_id = @AcademicCalendarId
                        AND del_status = FALSE";

            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(query, calendar);

            return result > 0;
        }

        public async Task<bool> DeleteAcademicCalendar(long id)
        {
            var query = @"UPDATE s_master.m_academic_calendar
                        SET
                        del_status = TRUE,
                        del_on_dt = CURRENT_TIMESTAMP
                        WHERE academic_calendar_id = @Id";

            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(query, new { Id = id });

            return result > 0;
        }
    }
}