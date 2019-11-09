using DEV_dashboard_2019.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Factory
{
    internal static class SteamApiClientFactory
    {
        private static Lazy<SteamApi> restClient = new Lazy<SteamApi>(
            () => new SteamApi(), LazyThreadSafetyMode.ExecutionAndPublication);

        static SteamApiClientFactory()
        {
        }

        public static SteamApi Instance
        {
            get
            {
                return restClient.Value;
            }
        } 
    }
}
