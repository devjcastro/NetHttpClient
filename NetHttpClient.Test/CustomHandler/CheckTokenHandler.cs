using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetHttpClient.Test
{
    public class CheckTokenHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var contentResult = new { messageStatus = "Usted no esta autorizado para solicitar este recurso" };
                response.Content = new StringContent(JsonConvert.SerializeObject(contentResult), Encoding.UTF8, "application/json");
            }
            return response;
        }

    }
}
