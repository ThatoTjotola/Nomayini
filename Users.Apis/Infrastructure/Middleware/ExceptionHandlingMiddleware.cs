using Users.Apis.Shared.Exceptions;

namespace Users.Apis.Infrastructure.Middleware;
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (ProblemDetailsException ex)
        {
            _logger.LogError(ex, "Problem details exception occurred");
            await HandleProblemDetailsException(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleUnhandledException(context);
        }
    }

    private static Task HandleProblemDetailsException(
        HttpContext context,
        ProblemDetailsException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        return context.Response.WriteAsJsonAsync(new
        {
            Type = exception.Title,
            exception.Title,
            Status = exception.StatusCode,
            Detail = exception.Message,
            exception.Extensions
        });
    }

    private static Task HandleUnhandledException(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsJsonAsync(new
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred"
        });
    }
}
