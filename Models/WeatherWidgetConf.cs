using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class WeatherWidgetConf
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "City Name")]
        public string CityName { get; set; }
    }
}
