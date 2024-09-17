namespace RealEstateApi.src.Api.Middlewares
{
    /// <summary>
    /// Middleware that measures the performance of each HTTP request by timing how long the request takes to process.
    /// If the request takes longer than 500 milliseconds, it logs a warning message.
    /// </summary>
    public class PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {

        private readonly RequestDelegate _next = next;
        private readonly ILogger<PerformanceMiddleware> _logger = logger;

        /// <summary>
        /// Invokes the middleware to handle the current HTTP request. Measures the time taken to process the request and logs a warning if it exceeds 500 milliseconds.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await _next(context);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (elapsedMs > 500)
            {
                _logger.LogWarning($"Request took {elapsedMs} ms to process.");
            }
        }
    }
}