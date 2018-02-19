using System;

namespace NetHttpClient.Http.Exceptions
{
    public class HttpUrlRequestException : Exception
    {
        public HttpUrlRequestException()
            : base() { }

        public HttpUrlRequestException(string message)
            : base(message) { }

        public HttpUrlRequestException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
