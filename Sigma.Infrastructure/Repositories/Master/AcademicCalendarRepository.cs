using Dapper;
using Sigma.Application.DTOs.Master;
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

        public async Task<IEnumerable<AcademicCalendarDto>> GetAllAcademicCalendars()
        {
            var query = @"SELECT
                ac.academic_calendar_id AS AcademicCalendarId,

                ac.academic_year_id AS AcademicYearId,
                ay.academic_year AS AcademicYearName,

                ac.school_id AS SchoolId,

                ac.class_id AS ClassId,
                c.class_name AS ClassName,

                ac.is_all_classes AS IsAllClasses,

                ac.event_type_id AS EventTypeId,
                et.event_type_name AS EventTypeName,

                ac.event_title AS EventTitle,
                ac.event_description AS EventDescription,

                ac.start_date AS StartDate,
                ac.end_date AS EndDate,
                ac.is_holiday AS IsHoliday

            FROM s_master.m_academic_calendar ac

            LEFT JOIN s_master.m_academic_year ay
            ON ac.academic_year_id = ay.academic_year_id
            AND ay.del_status = FALSE

            LEFT JOIN s_master.m_class c
            ON ac.class_id = c.class_id
            AND c.del_status = FALSE

            LEFT JOIN s_master.m_event_type et
            ON ac.event_type_id = et.event_type_id
            AND et.del_status = FALSE

            WHERE ac.del_status = FALSE

            ORDER BY ac.start_date";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<AcademicCalendarDto>(query);
        }
        public async Task<AcademicCalendarDto> GetAcademicCalendarById(long id)
        {
            var query = @"SELECT
            ac.academic_calendar_id AS AcademicCalendarId,

            ac.academic_year_id AS AcademicYearId,
            ay.academic_year AS AcademicYearName,

            ac.school_id AS SchoolId,

            ac.class_id AS ClassId,
            c.class_name AS ClassName,

            ac.is_all_classes AS IsAllClasses,

            ac.event_type_id AS EventTypeId,
            et.event_type_name AS EventTypeName,

            ac.event_title AS EventTitle,
            ac.event_description AS EventDescription,

            ac.start_date AS StartDate,
            ac.end_date AS EndDate,
            ac.is_holiday AS IsHoliday

        FROM s_master.m_academic_calendar ac

        LEFT JOIN s_master.m_academic_year ay
            ON ac.academic_year_id = ay.academic_year_id
            AND ay.del_status = FALSE

        LEFT JOIN s_master.m_class c
            ON ac.class_id = c.class_id
            AND c.del_status = FALSE

        LEFT JOIN s_master.m_event_type et
            ON ac.event_type_id = et.event_type_id
            AND et.del_status = FALSE

        WHERE ac.academic_calendar_id = @Id
        AND ac.del_status = FALSE";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<AcademicCalendarDto>(query, new { Id = id });
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