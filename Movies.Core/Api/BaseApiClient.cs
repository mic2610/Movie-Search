using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movies.Core.Api
{
    public class BaseApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        protected virtual HttpClient Client { get; set; } = client;

        protected virtual async Task<TResult> GetAsync<TResult>(string url, bool ensureSuccess = true)
        {
            var response = await Client.GetAsync(url);
            if (ensureSuccess)
                response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        protected virtual async Task<TResult> PostAsync<TContent, TResult>(string url, TContent content, MediaTypeFormatter formatter, bool ensureSuccess = true)
        {
            var response = await Client.PostAsync(url, content, formatter);
            if (ensureSuccess)
                response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        protected virtual async Task<TResult> PostAsJsonAsync<TContent, TResult>(string url, TContent value, bool ensureSuccess = true)
        {
            var response = await Client.PostAsJsonAsync(url, value);

            if (ensureSuccess)
                response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        protected virtual async Task<TResult> SendAsync<TResult>(HttpRequestMessage request, bool ensureSuccess = true)
        {
            var response = await Client.SendAsync(request);
            if (ensureSuccess)
                response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResult>();
        }

        protected virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool ensureSuccess = true)
        {
            var response = await Client.SendAsync(request);
            if (ensureSuccess)
                response.EnsureSuccessStatusCode();

            return response;
        }

        protected virtual async Task<TResult> SendAsync<TResult>(HttpMethod method, string url, HttpContent content = null, bool ensureSuccess = true)
        {
            using (var request = BuildRequest(method, url, content))
            {
                return await SendAsync<TResult>(request, ensureSuccess);
            }
        }

        protected virtual async Task<HttpStatusCode> SendAsync(HttpMethod method, string url, HttpContent content = null, bool ensureSuccess = true)
        {
            using (var request = BuildRequest(method, url, content))
            {
                var response = await SendAsync(request, ensureSuccess);
                return response.StatusCode;
            }
        }

        /// <remarks>This method is deliberately not virtual</remarks>
        protected HttpRequestMessage BuildRequest(HttpMethod method, string url, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url) { Method = method, RequestUri = new Uri(url) };

            // Add custom headers
            BuildRequestHeaders(request.Headers);

            if (content != null)
                request.Content = content;

            return request;
        }

        protected virtual void BuildRequestHeaders(HttpRequestHeaders headers)
        { }
    }
}
