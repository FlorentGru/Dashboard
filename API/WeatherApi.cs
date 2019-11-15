using DEV_dashboard_2019.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.API
{
    public class WeatherApi : ApiClient
    {
        public WeatherApi()
        {
            BaseEndpoint = new Uri("http://api.openweathermap.org/data/2.5/weather");
        }

        public async Task<WeatherModel> GetWeatherAsync(string city)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"q", city },
                {"APPID", "8cfb5a600e4411a9bc12dd489f8f6311" }
            };

            var requestUrl = CreateRequestUri("", queryParams);
            return await GetAsync<WeatherModel>(requestUrl);
        }
    }
}
