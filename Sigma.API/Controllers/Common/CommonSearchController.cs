using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces.Common;

namespace Sigma.API.Controllers.Common
{
    [ApiController]
    [Route("api/common-search")]
    public class CommonSearchController : ControllerBase
    {
        private readonly ICommonSearchService _service;

        public CommonSearchController(ICommonSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string schemaName,
            [FromQuery] string tableName,
            [FromQuery] string columnId,
            [FromQuery] string displayColumns,
            [FromQuery] string displayName,
            [FromQuery] string searchTerm = "",
            [FromQuery] string? otherCondition = null,
            [FromQuery] string? sortBy = null)
        {
            var result = await _service.SearchAsync(
                schemaName,
                tableName,
                columnId,
                displayColumns,
                displayName,
                searchTerm,
                otherCondition,
                sortBy);

            return Ok(result);
        }

    }
}
