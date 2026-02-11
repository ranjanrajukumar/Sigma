using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;

namespace Sigma.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IGlobalActivityLogService logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await logger.LogAsync(new GlobalActivityLog
                {
                    Level = "Error",
                    Service = "Sigma.API",
                    Source = context.Request.Path,
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Request = $"{context.Request.Method} {context.Request.Path}",
                    TraceId = context.TraceIdentifier
                });

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Internal Server Error",
                    TraceId = context.TraceIdentifier
                });
            }
        }
    }
}
