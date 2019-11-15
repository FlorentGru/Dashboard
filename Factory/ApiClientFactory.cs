using DEV_dashboard_2019.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Factory
{
    internal static class ApiClientFactory
    {
        private static Lazy<SteamApi> SteamRestClient = new Lazy<SteamApi>(
            () => new SteamApi(), LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<WeatherApi> WeatherRestClient = new Lazy<WeatherApi>(
            () => new WeatherApi(), LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
        }

        public static SteamApi SteamInstance
        {
            get
            {
                return SteamRestClient.Value;
            }
        }

        public static WeatherApi WeatherInstance
        {
            get
            {
                return WeatherRestClient.Value;
            }
        }
    }
}
