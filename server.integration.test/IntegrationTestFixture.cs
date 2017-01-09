using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace FH.Test.Integration
{
  public class ApiResponse<T>
  {
    public T Result { get; private set; }
    public HttpResponseMessage HttpResponse { get; private set; }

    public ApiResponse(T result, HttpResponseMessage httpResponse)
    {
      Result = result;
      HttpResponse = httpResponse;
    }
  }

  public class IntegrationTestFixture : IDisposable
  {
    public TestServer Server { get; }

    public IntegrationTestFixture()
    {
      Server = new TestServer(new WebHostBuilder().UseStartup<Server.Startup>().UseEnvironment("Test"));
    }

    public Task<ApiResponse<TResponse>> CallApi<TResponse>(string url, HttpMethod httpMethod)
        where TResponse : class
    {
      return CallApi<TResponse, object>(url, httpMethod);
    }

    public async Task<ApiResponse<TResponse>> CallApi<TResponse, TRequest>(string url, HttpMethod httpMethod, TRequest body = null)
        where TResponse : class
        where TRequest : class
    {
      using (var client = Server.CreateClient().AcceptJson())
      {
        HttpResponseMessage response;
        if (httpMethod == HttpMethod.Post)
        {
          response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(body)));
        }
        else if (httpMethod == HttpMethod.Put)
        {
          response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(body)));
        }
        else if (httpMethod == HttpMethod.Delete)
        {
          response = await client.DeleteAsync(url);
        }
        else
        {
          response = await client.GetAsync(url);
        }

        var result = response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created ?
            JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync()) :
            null;

        return new ApiResponse<TResponse>(result, response);
      }
    }

    public void Dispose()
    {
      Server.Dispose();
    }
  }
}
