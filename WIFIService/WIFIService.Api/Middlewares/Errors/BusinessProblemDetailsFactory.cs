using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WIFIService.Application.ResultPattern;

namespace WIFIService.Api.Middlewares.Errors;

public sealed class BusinessProblemDetailsFactory
{
    private readonly ProblemDetailsFactory _factory;

    public BusinessProblemDetailsFactory(ProblemDetailsFactory factory)
    {
        _factory = factory;
    }

    public ProblemDetails Create(HttpContext httpContext, Error error)
    {
        var statusCode = ErrorCodeMapper.ToStatusCode(error.Code);

        var problem = _factory.CreateProblemDetails(
            httpContext: httpContext,
            statusCode: statusCode,
            title: error.Code.ToString(),
            detail: error.Message,
            type: $"https://httpstatuses.com/{statusCode}",
            instance: httpContext.Request.Path
        );

        problem.Extensions["traceId"] = httpContext.TraceIdentifier;

        return problem;
    }
}
