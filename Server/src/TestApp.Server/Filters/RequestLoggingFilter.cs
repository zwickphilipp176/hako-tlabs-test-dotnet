using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TestApp.Server.Filters
{
    /// <summary>
    /// Logs incoming request details at Debug level, and logs any failures
    /// (action exceptions or non-success status codes) at Information level.
    /// </summary>
    public class RequestLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<RequestLoggingFilter> logger;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;

            logger.LogDebug(
                "Incoming request [{Method}] {Path}{QueryString} - RouteValues: {RouteValues}, Arguments: {@Arguments}",
                request.Method,
                request.Path,
                request.QueryString,
                context.RouteData.Values,
                context.ActionArguments);

            var executedContext = await next();

            if (executedContext.Exception != null && !executedContext.ExceptionHandled)
            {
                logger.LogError(
                    executedContext.Exception,
                    "Request [{Method}] {Path} failed with an unhandled exception",
                    request.Method,
                    request.Path);
            }
            else if (executedContext.Result is IStatusCodeActionResult statusCodeResult
                     && statusCodeResult.StatusCode >= 400)
            {
                logger.LogInformation(
                    "Request [{Method}] {Path} completed with status code {StatusCode}",
                    request.Method,
                    request.Path,
                    statusCodeResult.StatusCode);
            }
        }
    }
}
