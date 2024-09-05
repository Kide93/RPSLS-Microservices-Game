using System.Net;

namespace Shared.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ExternalServiceException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public ExternalServiceException(string message)
            : base(message)
        {
        }
    }
}
