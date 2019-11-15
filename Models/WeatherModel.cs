using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class WeatherModel
    {
        [DataMember(Name = "weather")]
        public IList<Weather> Weather { get; set; }

        [DataMember(Name = "main")]
        public Main Main { get; set; }

        [DataMember(Name = "clouds")]
        public Cloud Clouds { get; set; }

        [DataMember(Name = "dt")]
        public int Dt { get; set; }

        public DateTime ActualDate { get; set; }

        public string LongDate
        {
            get
            {
                return ActualDate.ToLongDateString();
            }
        }

        [DataMember(Name = "sys")]
        public Sys Sys { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cod")]
        public int Code { get; set; }

    }

    public class Weather
    {
        [DataMember(Name = "main")]
        public string Main { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }


    public class Main
    {
        [DataMember(Name = "temp")]
        public decimal Temp { get; set; }

        [DataMember(Name = "pressure")]
        public int Pressure { get; set; }

        [DataMember(Name = "humidity")]
        public int Humidity { get; set; }

        [DataMember(Name = "temp_min")]
        public decimal Temp_Min { get; set; }

        [DataMember(Name = "temp_max")]
        public decimal Temp_Max { get; set; }
    }

    public class Cloud
    {
        [DataMember(Name = "all")]
        public int All { get; set; }
    }

    public class Sys
    {
        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}
