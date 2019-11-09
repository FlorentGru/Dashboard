using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.API
{
    public class ApiClient
    {
        protected readonly HttpClient _httpClient;
        protected Uri BaseEndpoint { get; set; }

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        protected async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {

            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        protected HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected Uri CreateRequestUri(string relativePath, IDictionary<String, String> queryString)
        {
            var query = QueryHelpers.AddQueryString(relativePath, queryString);
            var endpoint = new Uri(BaseEndpoint, query);
            return endpoint;
        }

    }
}
