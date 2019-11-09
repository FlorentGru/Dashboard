using DEV_dashboard_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.API
{
    public class SteamApi : ApiClient
    {
        public SteamApi()
        {
            BaseEndpoint = new Uri("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/");
        }

        public async Task<SteamResponse> GetSteamId(string username)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"key", "EE0B4484350CF5909C19342297A18ADB" },
                {"vanityurl", username }
            };

            var requestUrl = CreateRequestUri("", queryParams);
            return await GetAsync<SteamResponse>(requestUrl);
        }
    }
}
