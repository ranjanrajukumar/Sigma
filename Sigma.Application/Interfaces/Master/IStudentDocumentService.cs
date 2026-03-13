using Sigma.Application.Common.Responses;
using Sigma.Application.DTOs.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Interfaces.Services.Master
{
    public interface IStudentDocumentService
    {
        Task<ApiResponse<long>> Create(CreateStudentDocumentDto dto);

        Task<ApiResponse<IEnumerable<StudentDocument>>> GetAll();

        Task<ApiResponse<StudentDocument?>> GetById(long id);

        Task<ApiResponse<bool>> Delete(long id);
    }
}