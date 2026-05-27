using WIFIService.Application.Responses;

namespace WIFIService.Api.Middlewares.Errors;

public static class ErrorCodeMapper
{
    public static int ToStatusCode(ErrorCode errorCode) => errorCode switch
    {
        ErrorCode.NotFound             => StatusCodes.Status404NotFound,
        ErrorCode.SpeedProfileNotFound => StatusCodes.Status404NotFound,
        _ => throw new InvalidOperationException($"No HTTP status mapped for ErrorCode '{errorCode}'.")
    };
}
