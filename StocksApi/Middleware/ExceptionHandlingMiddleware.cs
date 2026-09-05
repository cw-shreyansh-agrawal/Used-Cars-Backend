using System.Net;
using System.Text.Json;

namespace StocksApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; // The RequestDelegate represents the next middleware in the pipeline. It is used to invoke the next middleware component after the current one has completed its processing.
    private readonly ILogger<ExceptionHandlingMiddleware> _logger; // ASP.NET Core automatically provides ILogger<T> through dependency injection

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "An unexpected error occurred."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
