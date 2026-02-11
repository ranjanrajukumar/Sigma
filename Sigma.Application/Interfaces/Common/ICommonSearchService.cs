using Sigma.Application.DTOs.Common;

namespace Sigma.Application.Interfaces.Common
{
    public interface ICommonSearchService
    {
        Task<CommonSearchResponseDto> SearchAsync(
            string schemaName,
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string? otherCondition,
            string? sortBy);
    }
}
