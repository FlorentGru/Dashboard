using DEV_dashboard_2019.Factory;
using DEV_dashboard_2019.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.ViewComponents
{
    public class WeatherWidgetViewComponent : ViewComponent
    {
        public WeatherWidgetViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync(WeatherWidgetConf conf)
        {
            var response = await ApiClientFactory.WeatherInstance.GetWeatherAsync(conf.CityName);

            //            if (response.Code != 200)
            response.ActualDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            response.ActualDate = response.ActualDate.AddSeconds(response.Dt).ToLocalTime();

            response.Main.Temp = Decimal.Subtract(response.Main.Temp, 273.15M);
            response.Main.Temp_Max = Decimal.Subtract(response.Main.Temp_Max, 273.15M);
            response.Main.Temp_Min = Decimal.Subtract(response.Main.Temp_Min, 273.15M);
            ViewData["Icon"] = "http://openweathermap.org/img/wn/" + response.Weather.First().Icon + "@2x.png";
            ViewData["Id"] = conf.UserId;
            return View(response);
        }
    }
}
