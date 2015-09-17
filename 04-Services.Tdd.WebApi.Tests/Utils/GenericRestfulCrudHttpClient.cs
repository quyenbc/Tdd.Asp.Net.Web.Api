using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NUnit.Framework;

namespace _04_Services.Tdd.WebApi.Tests.Utils
{
    /// <summary>
    /// NOT IN USE --- probably old....
    /// 
    /// This class exposes RESTful CRUD functionality in a generic way, abstracting
    /// the implementation and useage details of HttpClient, HttpRequestMessage,
    /// HttpResponseMessage, ObjectContent, Formatters etc. 
    /// </summary>
    /// <typeparam name="T">This is the Type of Resource you want to work with, such as Customer, Order etc.</typeparam>
    /// <typeparam name="TResourceIdentifier">This is the type of the identifier that uniquely identifies a specific resource such as Id or Username etc.</typeparam>
    public class GenericRestfulCrudHttpClient<T, TResourceIdentifier> : IDisposable where T : class
    {
        private bool disposed = false;
        private HttpClient httpClient;
        protected readonly string serviceBaseAddress;
        private readonly string addressSuffix;
        private readonly string jsonMediaType = "application/json";

        /// <summary>
        /// The constructor requires two parameters that essentially initialize the underlying HttpClient.
        /// In a RESTful service, you might have URLs of the following nature (for a given resource - Member in this example):<para />
        /// 1. http://www.somedomain/api/members/<para />
        /// 2. http://www.somedomain/api/members/jdoe<para />
        /// Where the first URL will GET you all members, and allow you to POST new members.<para />
        /// While the second URL supports PUT and DELETE operations on a specifc member.
        /// </summary>
        /// <param name="serviceBaseAddress">As per the example, this would be "http://www.somedomain"</param>
        /// <param name="addressSuffix">As per the example, this would be "api/members/"</param>

        public GenericRestfulCrudHttpClient(string serviceBaseAddress, string addressSuffix)
        {
            this.serviceBaseAddress = serviceBaseAddress;
            this.addressSuffix = addressSuffix;
            httpClient = MakeHttpClient(serviceBaseAddress);
        }

        protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Matlus_HttpClient", "1.0")));
            return httpClient;
        }

        public async Task<IEnumerable<T>> GetManyAsync()
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> GetAsync(TResourceIdentifier identifier)
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix + identifier.ToString());
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsAsync<T>();
        }

        public async Task<T> PostAsync(T model)
        {
            var requestMessage = new HttpRequestMessage();
            var responseMessage = await httpClient.PostAsJsonAsync(addressSuffix, model);
            return await responseMessage.Content.ReadAsAsync<T>();
        }

        public async Task PutAsync(TResourceIdentifier identifier, T model)
        {
            var requestMessage = new HttpRequestMessage();
            var responseMessage = await httpClient.PutAsJsonAsync(addressSuffix + identifier.ToString(), model);
        }

        public async Task DeleteAsync(TResourceIdentifier identifier)
        {
            var r = await httpClient.DeleteAsync(addressSuffix + identifier.ToString());
        }


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                {
                    var hc = httpClient;
                    httpClient = null;
                    hc.Dispose();
                }
                disposed = true;
            }
        }

        #endregion IDisposable Members
    }
}
