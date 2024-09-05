namespace Shared.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExternalServiceException(string message)
            : base(message)
        {
        }
    }
}
