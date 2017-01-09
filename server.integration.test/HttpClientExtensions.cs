using System.Net.Http;
using System.Net.Http.Headers;

namespace FH.Test.Integration
{
    public static class HttpClientExtensions
    {
        public static HttpClient AcceptJson(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
