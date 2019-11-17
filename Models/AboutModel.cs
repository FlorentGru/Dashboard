using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class AboutModel
    {
        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("server")]
        public Server Server { get; set; }
    }

    public class Client
    {
        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public class Server
    {
        [JsonProperty("current_time")]
        public Int32 CurrentTime { get; set; }

        [JsonProperty("services")]
        public List<Service> Services { get; set; }
    }

    public class Service
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("widgets")]
        public List<Widget> Widgets { get; set; }
    }

    public class Widget
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("params")]
        public List<Param> Params { get; set; }
    }

    public class Param
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
