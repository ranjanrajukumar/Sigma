using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Services.Master
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository _repo;

        public AcademicYearService(IAcademicYearRepository repo)
        {
            _repo = repo;
        }

        // ================================
        // GET ALL
        // ================================
        public async Task<IEnumerable<AcademicYear>> GetAllAsync()
        {
            return await _repo.GetAllWithTermsAsync();
        }
        // ================================
        // GET BY ID WITH TERMS
        // ================================
        public async Task<AcademicYear?> GetByIdAsync(long id)
        {
            return await _repo.GetAcademicYearWithTerms(id);
        }

        // ================================
        // CREATE
        // ================================
        public async Task<string> CreateAsync(AcademicYearCreateDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
                return "Start date must be less than end date";

            if (await _repo.ExistsAsync(dto.AcademicYearName))
                return "Academic year already exists";

            var academicYear = new AcademicYear
            {
                AcademicYearName = dto.AcademicYearName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = false
            };

            // insert academic year
            var academicYearId = await _repo.CreateAsync(academicYear);

            // insert terms
            if (dto.Terms != null && dto.Terms.Count > 0)
            {
                var terms = dto.Terms.Select(t => new AcademicYearTerm
                {
                    AcademicYearId = academicYearId,
                    TermName = t.TermName,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    WorkingDays = t.WorkingDays
                }).ToList();

                await _repo.InsertTermsAsync(terms);
            }

            return "Academic year with terms created successfully";
        }

        // ================================
        // UPDATE ACADEMIC YEAR + TERMS
        // ================================
        public async Task<string> UpdateAsync(AcademicYearUpdateDto dto)
        {
            var entity = new AcademicYear
            {
                AcademicYearId = dto.AcademicYearId,
                AcademicYearName = dto.AcademicYearName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            var result = await _repo.UpdateAsync(entity);

            if (!result)
                return "Update failed";

            if (dto.Terms != null)
            {
                foreach (var term in dto.Terms)
                {
                    if (term.TermId > 0)
                    {
                        // UPDATE TERM
                        await _repo.UpdateTermAsync(new AcademicYearTerm
                        {
                            TermId = term.TermId,
                            TermName = term.TermName,
                            StartDate = term.StartDate,
                            EndDate = term.EndDate,
                            WorkingDays = term.WorkingDays
                        });
                    }
                    else
                    {
                        // INSERT NEW TERM
                        await _repo.InsertTermAsync(new AcademicYearTerm
                        {
                            AcademicYearId = dto.AcademicYearId,
                            TermName = term.TermName,
                            StartDate = term.StartDate,
                            EndDate = term.EndDate,
                            WorkingDays = term.WorkingDays
                        });
                    }
                }
            }

            return "Academic year updated successfully";
        }

        // ================================
        // DELETE
        // ================================
        public async Task<string> DeleteAsync(long id)
        {
            var result = await _repo.DeleteAsync(id);

            return result ? "Deleted successfully" : "Delete failed";
        }
    }
}