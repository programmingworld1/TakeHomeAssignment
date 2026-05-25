using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using WIFIService.Application.Errors;

namespace WIFIService.Api.Middlewares
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problem = exception switch
            {
                HttpException httpEx => CreateProblem(
                    context,
                    MapStatusCode(httpEx.ErrorCode),
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

        private int MapStatusCode(string errorCode)
        => errorCode switch
        {
            "NotFound" => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };

        private ProblemDetails CreateProblem(
            HttpContext ctx,
            int statusCode,
            string exceptionMessage,
            string exceptionErrorCode)
        {
            var pd = new ProblemDetails
            {
                Status = statusCode, // If present, the "status" member MUST be the HTTP status code.
                                     // Imagine you app throws a 409 conflict, 409 is statuscode, title could be "EmailAlreadyExists, SongAlreadyExists, ImportDuplicate", so with title you add different options per statusCode.
                                     // You need this because the frontend could use it for logic, if you used "Detail field", well that field could change, the language could bedifferent, etc.. that is why you need an Enum-Like-field which is the Title.
                Title = exceptionErrorCode, // If present, the "title" member MUST be a short, human-readable summary of the problem type.
                Type = $"https://httpstatuses.com/{statusCode}", // If present, the "type" member MUST be a URI reference that identifies the problem type.
                Detail = exceptionMessage, // If present, the "detail" member MUST be a human-readable explanation specific to this occurrence of the problem.
                Instance = ctx.Request.Path // If present, the "instance" member MUST be a URI reference that identifies the specific occurrence of the problem.
            };

            // 7807 RFC says: You MAY include additional members in the problem details object. So Microsoft added Extensions
            // In ASP.NET Core each HTTP request has an unique ID (TraceIdentifier) that gets generated for each request
            // So when the client says "I got an error during..." support can then ask for the TraceID instead of digging in 50000 records logs generated in 1 min for example. Now backend and frontend logs can also correlate.
            // This field is no mandatory for 7807 but it helps.
            pd.Extensions["traceId"] = ctx.TraceIdentifier;

            // Example body respone:
            // {
            // "type": "https://httpstatuses.com/404",
            // "title": "SongNotFound",
            // "status": 404,
            // "detail": "Song '123' was not found.",
            // "instance": "/api/songs/123"
            // }

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
}
