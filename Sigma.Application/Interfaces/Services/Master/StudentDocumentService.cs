using Sigma.Application.Common.Constants;
using Sigma.Application.Common.Responses;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Application.Interfaces.Services.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Services.Master
{
    public class StudentDocumentService : IStudentDocumentService
    {
        private readonly IStudentDocumentRepository _repository;

        public StudentDocumentService(IStudentDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<long>> Create(CreateStudentDocumentDto dto)
        {
            var entity = new StudentDocument
            {
                DocumentName = dto.DocumentName,
                DocumentCode = dto.DocumentCode,
                Description = dto.Description,
                IsMandatory = dto.IsMandatory,
                IsActive = dto.IsActive,
                AuthAdd = "admin"
            };

            return await _repository.CreateStudentDocument(entity);
        }

        public async Task<ApiResponse<IEnumerable<StudentDocument>>> GetAll()
        {
            var data = await _repository.GetAllStudentDocuments();

            return new ApiResponse<IEnumerable<StudentDocument>>(true, ApiMessages.Success, data);
        }

        public async Task<ApiResponse<StudentDocument?>> GetById(long id)
        {
            var data = await _repository.GetStudentDocumentById(id);

            if (data == null)
                return new ApiResponse<StudentDocument?>(false, ApiMessages.NotFound, null);

            return new ApiResponse<StudentDocument?>(true, ApiMessages.Success, data);
        }

        public async Task<ApiResponse<bool>> Delete(long id)
        {
            var result = await _repository.DeleteStudentDocument(id);

            if (!result)
                return new ApiResponse<bool>(false, ApiMessages.NotFound, false);

            return new ApiResponse<bool>(true, ApiMessages.Deleted, true);
        }
    }
}