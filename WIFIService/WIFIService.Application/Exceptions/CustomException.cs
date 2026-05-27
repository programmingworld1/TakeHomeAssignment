using WIFIService.Application.ResultPattern;

namespace WIFIService.Application.Exceptions;

public abstract class CustomException : Exception
{
    public ErrorCode ErrorCode { get; }

    protected CustomException(ErrorCode errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}
