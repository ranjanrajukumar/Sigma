using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.Application.Services.Master
{
    public class MClassService : IMClassService
    {
        private readonly IMClassRepository _repository;

        public MClassService(IMClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MClass>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MClass?> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Class Id");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> CreateAsync(CreateClassDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ClassName))
                return "Class Name is required";

            if (dto.ClassOrder <= 0)
                return "Class Order must be greater than 0";

            var entity = new MClass
            {
                ClassName = dto.ClassName,
                ClassOrder = dto.ClassOrder,
               
            };

            await _repository.CreateAsync(entity);
            return "Class created successfully";
        }

        public async Task<string> UpdateAsync(UpdateClassDto dto)
        {
            if (dto.ClassId <= 0)
                return "Invalid Class Id";

            var entity = new MClass
            {
                ClassId = dto.ClassId,
                ClassName = dto.ClassName,
                ClassOrder = dto.ClassOrder
                
            };

            await _repository.UpdateAsync(entity);
            return "Class updated successfully";
        }

        public async Task<string> DeleteAsync(long id)
        {
            if (id <= 0)
                return "Invalid Class Id";

            await _repository.DeleteAsync(id);
            return "Class deleted successfully";
        }
    }
}
