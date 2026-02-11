using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;
using Sigma.Domain.ValueObjects;

namespace Sigma.API.Middleware
{
    public class GlobalActivityLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalActivityLoggingMiddleware(RequestDelegate next)
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
                    Level = ActivityLogLevel.Error,
                    Service = "YourApp.API",
                    Source = context.Request.Path,
                    Message = ex.Message,
                    Exception = ex,
                    Request = new
                    {
                        context.Request.Method,
                        context.Request.Path,
                        context.Connection.RemoteIpAddress
                    }
                });

                throw;
            }
        }
    }
}
