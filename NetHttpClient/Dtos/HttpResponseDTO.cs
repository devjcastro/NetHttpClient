using System.Net;

namespace NetHttpClient.Http.Response
{
    public class HttpResponseDTO<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
    }
}
