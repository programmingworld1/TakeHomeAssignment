using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WIFIService.Application.Exceptions;

namespace WIFIService.Api.Middlewares.Errors;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception on {Method} {Path}",
            context.Request.Method, context.Request.Path);

        var problem = exception switch
        {
            CustomException httpEx => CreateProblem(
                context,
                ErrorCodeMapper.ToStatusCode(httpEx.ErrorCode),
                httpEx.Message,
                httpEx.ErrorCode
            ),

            ValidationException valEx => CreateValidationProblem(
                context,
                valEx
            ),

            _ => CreateProblem(
                context,
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                "InternalServerError"
            )
        };

        context.Response.StatusCode = problem.Status!.Value;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblem(
        HttpContext ctx,
        int statusCode,
        string exceptionMessage,
        string exceptionErrorCode)
    {
        var pd = new ProblemDetails
        {
            Status = statusCode,
            Title = exceptionErrorCode,
            Type = $"https://httpstatuses.com/{statusCode}",
            Detail = exceptionMessage,
            Instance = ctx.Request.Path
        };

        pd.Extensions["traceId"] = ctx.TraceIdentifier;

        return pd;
    }

    private ProblemDetails CreateValidationProblem(
        HttpContext ctx,
        ValidationException ex)
    {
        var errors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var pd = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "ValidationFailed",
            Type = "https://httpstatuses.com/400",
            Detail = ex.Message,
            Instance = ctx.Request.Path
        };

        pd.Extensions["traceId"] = ctx.TraceIdentifier;

        return pd;
    }
}
