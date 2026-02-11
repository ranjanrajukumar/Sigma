using Sigma.Application.DTOs.Common;
using Sigma.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Services.Common
{
    public class CommonSearchService : ICommonSearchService
    {
        private readonly ICommonSearchRepository _repository;

        public CommonSearchService(ICommonSearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommonSearchResponseDto> SearchAsync(
            string schemaName,
            string tableName,
            string columnId,
            string displayColumns,
            string displayName,
            string searchTerm,
            string? otherCondition,
            string? sortBy)
        {
            return await _repository.SearchAsync(
                schemaName,
                tableName,
                columnId,
                displayColumns,
                displayName,
                searchTerm,
                otherCondition,
                sortBy);
        }
    }
}
