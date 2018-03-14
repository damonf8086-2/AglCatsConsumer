using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CatsConsumer.Interfaces;

namespace CatsConsumer
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(string message)
            : base(message)
        {
        }

        public HttpResponseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetStringAsync(string uri)
        {
            using (var response = await _httpClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpResponseException($"HTTP response status indicates error: {(int)response.StatusCode}-{response.StatusCode}");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
