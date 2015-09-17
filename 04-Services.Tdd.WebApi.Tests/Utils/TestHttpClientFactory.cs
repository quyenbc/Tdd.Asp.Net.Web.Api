using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.SelfHost;
using _04_Services.WebApi;


//using _04_Services.WebApi;

namespace _04_Services.Tdd.WebApi.Tests.Utils
{
    public class TestHttpClientFactory
    {
        public static HttpClient Create()
        {
            HttpClient client = null;
            try
            {
                //configure the base address
                var baseAddress = new Uri("http://localhost:9999");

                //set the base address to new configuration
                var configuration = new HttpSelfHostConfiguration(baseAddress);

                //register configuration at the web api
                WebApiConfig.Register(configuration);
                
                //create self-hosting server with our configuration
                var server = new HttpSelfHostServer(configuration);


                // Start listening
                //server.OpenAsync().Wait();
                
                //create a new client
                client = new HttpClient(server);
                client.BaseAddress = baseAddress;
                //2. Accept headers in the request
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //3. Content type of the body
                

                return client;
            }
            catch (Exception)
            {
                client?.Dispose();
                throw;
            }
        }
    }
}