using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;
using Sigma.Domain.ValueObjects;
using System.Text.Json;

namespace Sigma.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IGlobalActivityLogService logger)
        {
            var startTime = DateTime.UtcNow;

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            var requestInfo = JsonSerializer.Serialize(new
            {
                Method = context.Request.Method,
                Path = context.Request.Path.ToString(),
                StatusCode = context.Response.StatusCode,
                DurationMs = duration.TotalMilliseconds,
                RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString()
            });

            await logger.LogAsync(new GlobalActivityLog
            {
                Level = ActivityLogLevel.Info,
                Service = "Sigma.API",
                Source = context.Request.Path,
                Message = "Request Executed",
                Request = requestInfo,   // ✅ FIXED
                TraceId = context.TraceIdentifier
            });
        }
    }
}
