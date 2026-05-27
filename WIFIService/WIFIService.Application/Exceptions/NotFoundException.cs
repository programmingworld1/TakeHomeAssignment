using WIFIService.Application.Responses;

namespace WIFIService.Application.Exceptions
{
    public abstract class NotFoundException : CustomException
    {
        protected NotFoundException(ErrorCode errorCode, string message)
            : base(errorCode, message) { }
    }
}
