using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Sigma.Domain.Entities.Utilities;

namespace Sigma.API.Controllers.Utilities
{

    [ApiController]
    [Route("api/logs")]
    public class GlobalActivityLogsController : ControllerBase
    {
        private readonly IMongoCollection<GlobalActivityLog> _collection;

        public GlobalActivityLogsController(IMongoDatabase database)
        {
            _collection = database.GetCollection<GlobalActivityLog>("global_activity_logs");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var logs = await _collection
                .Find(_ => true)
                .SortByDescending(x => x.CreatedAt)
                .Limit(100)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
