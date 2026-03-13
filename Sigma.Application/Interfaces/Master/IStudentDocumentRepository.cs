using Sigma.Application.Common.Responses;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Interfaces.Master
{
    public interface IStudentDocumentRepository
    {
        Task<ApiResponse<long>> CreateStudentDocument(StudentDocument entity);

        Task<IEnumerable<StudentDocument>> GetAllStudentDocuments();

        Task<StudentDocument?> GetStudentDocumentById(long id);

        Task<bool> UpdateStudentDocument(StudentDocument entity);

        Task<bool> DeleteStudentDocument(long id);
    }
}