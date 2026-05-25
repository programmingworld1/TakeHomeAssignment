using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WIFIService.Application.Results;

namespace WIFIService.Api.Middlewares
{
    public sealed class BusinessProblemDetailsFactory
    {
        private readonly ProblemDetailsFactory _factory;

        public BusinessProblemDetailsFactory(ProblemDetailsFactory factory)
        {
            _factory = factory;
        }

        public ProblemDetails Create(HttpContext httpContext, Error error)
        {
            var statusCode = MapStatusCode(error.Code);

            var problem = _factory.CreateProblemDetails(
                httpContext: httpContext,
                statusCode: statusCode,
                title: error.Code,
                detail: error.Message,
                type: $"https://httpstatuses.com/{statusCode}",
                instance: httpContext.Request.Path
            );

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            return problem;
        }

        private static int MapStatusCode(string errorCode)
            => errorCode switch
            {
                "ArtistNotFound" => StatusCodes.Status404NotFound,
                "SongNotFound" => StatusCodes.Status404NotFound,
                "EmailAlreadyExists" => StatusCodes.Status409Conflict,
                "WrongEmail" => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };
    }
}
