namespace WIFIService.Api.Middlewares.Errors;

public static class ErrorCodeMapper
{
    public static int ToStatusCode(string errorCode) => errorCode switch
    {
        "NotFound"             => StatusCodes.Status404NotFound,
        "SpeedProfileNotFound" => StatusCodes.Status404NotFound,
        _                      => StatusCodes.Status400BadRequest
    };
}
