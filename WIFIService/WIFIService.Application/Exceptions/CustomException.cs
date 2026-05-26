namespace WIFIService.Application.Exceptions
{
    public abstract class CustomException : Exception
    {
        public string ErrorCode { get; }

        protected CustomException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
