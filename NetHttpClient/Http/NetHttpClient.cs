using NetHttpClient.Http.Exceptions;
using NetHttpClient.Http.Response;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetHttpClient.Http
{
    public class NetHttpClient
    {


        #region Delegates
        public delegate void OnResponseEvent(HttpResponseDTO<object> response);
        public delegate void OnFailureEvent(HttpResponseDTO<object> response);
        #endregion


        #region Properties
        public string BaseAddress { get; set; }
        public string RequestUri { get; set; }
        public string FullUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public HttpClient HttpClient { get; set; }
        public HttpVerb HttpVerb { get; set; }
        public HttpContent HttpContent { get; set; }
        public OnResponseEvent OnResponse { get; set; }
        public OnFailureEvent OnFailure { get; set; }
        public HttpMessageHandler Handler { get; set; }
        #endregion



        #region attributes
        private dynamic payload;
        #endregion


        public NetHttpClient(NetHttpClientBuilder builder)
        {
            Init(builder);
            ConfigureHttpClient();
        }


        private void Init(NetHttpClientBuilder builder)
        {
            BaseAddress = builder._baseAddress;
            RequestUri = builder._requestUri;
            FullUrl = builder._fullUrl;
            HttpVerb = builder._httpVerb;
            payload = builder._payload;
            Timeout = builder._timeout;
            OnResponse = builder._onResponseEvent;
            OnFailure = builder._onFailureEvent;
            Handler = builder._handler;
        }



        public static NetHttpClientBuilder Builder(NetHttpClientBuilder httpClientRestBuilder = null)
        {
            if (httpClientRestBuilder != null)
            {
                return httpClientRestBuilder;
            }
            return new NetHttpClientBuilder();
        }
        

        private void ConfigureHttpClient()
        {
            if (Handler != null)
                HttpClient = new HttpClient(Handler);
            else
                HttpClient = new HttpClient();


            if (BaseAddress != null)
                HttpClient.BaseAddress = new Uri(BaseAddress);


            if (Timeout != TimeSpan.Zero)
                HttpClient.Timeout = Timeout;


            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "=" + serverKey);
            HttpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        }



        public async Task<HttpResponseDTO<T>> ConsumeAsync<T>() where T : class
        {

            CheckExeptions();

            HttpResponseDTO<T> responseDto = new HttpResponseDTO<T>();

            string urlRequest = FullUrl ?? RequestUri;

            HttpResponseMessage response = null;

            try
            {
                response = GetHttpResponse(urlRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }


            string jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            responseDto.StatusCode = response.StatusCode;
            if (response.StatusCode == (HttpStatusCode)401)
            {
                dynamic res = JsonConvert.DeserializeObject(jsonString);
                if(res != null)
                    responseDto.Message = res.messageStatus;
            }
            else
            {
                T dynamicObj = JsonConvert.DeserializeObject<T>(jsonString);
                responseDto.Body = dynamicObj;
            }

            return responseDto;
        }


        /*Definition for Callback Methods*/
        public async void Call<T>()
        {

            CheckExeptions();
            HttpResponseDTO<object> responseDto = new HttpResponseDTO<object>();
            string urlRequest = FullUrl ?? RequestUri;

            HttpResponseMessage response = null;

            try
            {
                response = GetHttpResponse(urlRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

            string jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            responseDto.StatusCode = response.StatusCode;
            if (response.StatusCode == (HttpStatusCode)401)
            {
                responseDto.Message = "You are not authorized to request this resource";
                OnFailure?.Invoke(responseDto);
            }
            else
            {
                T dynamicObj = JsonConvert.DeserializeObject<T>(jsonString);
                responseDto.Body = dynamicObj;
                OnResponse?.Invoke(responseDto);
            }

        }


        private HttpResponseMessage GetHttpResponse(string urlRequest)
        {
            HttpResponseMessage response = null;
            try
            {
                switch (HttpVerb)
                {
                    case HttpVerb.GET:
                        response = HttpClient.GetAsync(urlRequest).Result;
                        break;
                    case HttpVerb.POST:
                        response = HttpClient.PostAsync(urlRequest, HttpContent).Result;
                        break;
                    case HttpVerb.PUT:
                        response = HttpClient.PutAsync(urlRequest, HttpContent).Result;
                        break;
                    case HttpVerb.DELETE:
                        response = HttpClient.DeleteAsync(urlRequest).Result;
                        break;
                    default:
                        response = HttpClient.GetAsync(urlRequest).Result;
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            return response;
        }


        public void CheckExeptions()
        {
            if (!String.IsNullOrEmpty(FullUrl) && !String.IsNullOrEmpty(BaseAddress))
                throw new HttpUrlRequestException("No se puede tener configurado la propiedad BaseAddress cuando se establecion un valor para la propiedad FullUrl");

            else if (!String.IsNullOrEmpty(FullUrl) && !String.IsNullOrEmpty(RequestUri))
                throw new HttpUrlRequestException("No se puede tener configurado la propiedad RequestUri cuando se establecion un valor para la propiedad FullUrl");
        }


    }
}
