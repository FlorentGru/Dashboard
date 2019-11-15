 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel()
        {
            WeatherConf = new List<WeatherWidgetConf>();
        }

        public IEnumerable<WeatherWidgetConf> WeatherConf;
    }
}
