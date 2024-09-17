using System.Net;
using System.Text.Json;

namespace RealEstateApi.src.Api.Middlewares
{
    /// <summary>
    /// Global middleware for handling exceptions in the application.
    /// Catches all unhandled exceptions and returns a standard error response
    /// with an HTTP status code 500.
    /// </summary>
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        /// <summary>
        /// Invokes the middleware. This method catches any exceptions thrown in the HTTP request pipeline
        /// and handles them by returning a JSON error message.
        /// </summary>
        /// <param name="httpContext">The current HTTP request context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something wen wront: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles the thrown exception and generates an HTTP response with a standard error message in JSON format.
        /// </summary>
        /// <param name="context">The current HTTP request context.</param>
        /// <param name="exception">The exception that was thrown during request execution.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of writing the response.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                context.Response.StatusCode,
                Message = "An unexpected error occured. Please try again later.",
                ErrorId = Guid.NewGuid(),
                Details = exception.Message
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}