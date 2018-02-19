using System;
using static NetHttpClient.Http.NetHttpClient;

namespace NetHttpClient.Http
{
    public class NetHttpClientBuilder
    {
        #region Properties
        public string _baseAddress;
        public string _requestUri;
        public string _fullUrl;
        public TimeSpan _timeout;
        public HttpVerb _httpVerb;
        public dynamic _payload;
        public OnResponseEvent _onResponseEvent;
        public OnFailureEvent _onFailureEvent;
        #endregion


        public NetHttpClientBuilder BaseAddress(string baseAddress)
        {
            _baseAddress = baseAddress;
            return this;
        }

        public NetHttpClientBuilder Requesturi(string requestUri)
        {
            _requestUri = requestUri;
            return this;
        }

        public NetHttpClientBuilder FullUrl(string fullUrl)
        {
            _fullUrl = fullUrl;
            return this;
        }

        public NetHttpClientBuilder TimeOut(TimeSpan timeout)
        {
            _timeout = timeout;
            return this;
        }

        public NetHttpClientBuilder HttpMethod(HttpVerb httpVerb)
        {
            _httpVerb = httpVerb;
            return this;
        }


        public NetHttpClientBuilder HttpGet()
        {
            _httpVerb = HttpVerb.GET;
            return this;
        }

        public NetHttpClientBuilder HttpPost()
        {
            _httpVerb = HttpVerb.POST;
            return this;
        }

        public NetHttpClientBuilder HttpPut()
        {
            _httpVerb = HttpVerb.PUT;
            return this;
        }

        public NetHttpClientBuilder HttpDelete()
        {
            _httpVerb = HttpVerb.DELETE;
            return this;
        }

        public NetHttpClientBuilder Payload(dynamic payload)
        {
            _payload = payload;
            return this;
        }

        public NetHttpClientBuilder OnSuccessEvent(OnResponseEvent onSuccessEvent)
        {
            _onResponseEvent = onSuccessEvent;
            return this;
        }

        public NetHttpClientBuilder OnFailureEvent(OnFailureEvent onFailureEvent)
        {
            _onFailureEvent = onFailureEvent;
            return this;
        }

        public NetHttpClient Build()
        {
            return new NetHttpClient(this);
        }

    }
}
