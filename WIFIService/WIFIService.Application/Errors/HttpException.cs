namespace WIFIService.Application.Errors
{
    public abstract class HttpException : Exception
    {
        public string ErrorCode { get; }

        protected HttpException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
