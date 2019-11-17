using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DEV_dashboard_2019.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DEV_dashboard_2019.Controllers
{
    public class AboutController : Controller
    {
        [Route("about.json")]
        public string Index()
        {
            var localIpAddress = GetIPAddress();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var aboutModel = new AboutModel();

            aboutModel.Client = new Client
            {
                Host = localIpAddress
            };

            aboutModel.Server = new Server
            {
                CurrentTime = unixTimestamp,
                Services = new List<Service>()
            };

            var weatherService = new Service
            {
                Name = "weather",
                Widgets = new List<Widget>()
            };

            var weatherWidget = new Widget
            {
                Name = "city_weather",
                Description = "Display weather informations for a given city",
                Params = new List<Param>()
            };

            var city = new Param
            {
                Name = "city",
                Type = "string"
            };

            var steamService = new Service
            {
                Name = "steam",
                Widgets = new List<Widget>()
            };

            var achievementWidget = new Widget
            {
                Name = "achievement_list",
                Description = "Display the achieved achievements of a user for a given game ID",
                Params = new List<Param>()
            };

            var steamUser = new Param
            {
                Name = "steam_user",
                Type = "string"
            };

            var appId = new Param
            {
                Name = "app_id",
                Type = "integer"
            };

            var friendListWidget = new Widget
            {
                Name = "friend_list",
                Description = "Display the friend list of a user telling if for each friend if they are logged or playing",
                Params = new List<Param>()
            };

            var movieService = new Service
            {
                Name = "movie",
                Widgets = new List<Widget>()
            };

            var movieSearchWidget = new Widget
            {
                Name = "movie_search",
                Description = "Display the related movies overview for a given movie query and year",
                Params = new List<Param>()
            };

            var movieName = new Param
            {
                Name = "movie_name",
                Type = "string"
            };

            var year = new Param
            {
                Name = "year",
                Type = "integer"
            };

            var trendWidget = new Widget
            {
                Name = "trend_list",
                Description = "Display the trend of movies or tv shows overview of the day or the week",
                Params = new List<Param>()
            };

            var media = new Param
            {
                Name = "media",
                Type = "string"
            };

            var timeWindow = new Param
            {
                Name = "time_window",
                Type = "string"
            };

            var movieDetailsWidget = new Widget
            {
                Name = "movie_details",
                Description = "Display the details of a movie for a given movie name",
                Params = new List<Param>()
            };

            weatherWidget.Params.Add(city);

            weatherService.Widgets.Add(weatherWidget);

            achievementWidget.Params.Add(steamUser);
            achievementWidget.Params.Add(appId);

            friendListWidget.Params.Add(steamUser);

            steamService.Widgets.Add(achievementWidget);
            steamService.Widgets.Add(friendListWidget);

            movieSearchWidget.Params.Add(movieName);

            trendWidget.Params.Add(media);
            trendWidget.Params.Add(timeWindow);

            movieDetailsWidget.Params.Add(movieName);

            movieService.Widgets.Add(movieSearchWidget);
            movieService.Widgets.Add(trendWidget);
            movieService.Widgets.Add(movieDetailsWidget);

            aboutModel.Server.Services.Add(weatherService);
            aboutModel.Server.Services.Add(steamService);
            aboutModel.Server.Services.Add(movieService);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(aboutModel, Formatting.Indented);

            return json;
        }
        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            string ipaddress = ""; 
            Hostname = Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipaddress = Convert.ToString(IP);
                }
            }
            return (ipaddress);
        }
    }
}