using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace IsconGathiya.Common
{
    public static class HttpHelper
    {

        private static IHttpContextAccessor _httpContextAccessor;
        private static readonly HttpClient _httpClient = new HttpClient();
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        public static async Task<string> CommanCallApi(string baseurl, object requestBody, string? accessToken = null)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseurl)
            {
                Content = jsonContent
            };
            if (!string.IsNullOrEmpty(accessToken))
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
