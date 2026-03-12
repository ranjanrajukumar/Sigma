using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sigma.Application.Interfaces.Master
{
    public interface IAcademicCalendarRepository
    {
        Task<long> CreateAcademicCalendar(AcademicCalendar calendar);

        Task<IEnumerable<AcademicCalendar>> GetAllAcademicCalendars();

        Task<AcademicCalendar> GetAcademicCalendarById(long id);

        Task<bool> UpdateAcademicCalendar(AcademicCalendar calendar);

        Task<bool> DeleteAcademicCalendar(long id);
    }
}