using DEV_dashboard_2019.Models;
using DEV_dashboard_2019.Models.WidgetConf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Trending;

namespace DEV_dashboard_2019.ViewComponents
{
    public class TrendWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TrendWidgetConf widgetConf)
        {
            var client = new TMDbClient("bcf08ea1238c643647ae3e34a0fd4d75");

            ViewData["Error"] = "";
            ViewData["Time"] = widgetConf.Time;
            ViewData["Id"] = widgetConf.Id;

            var result = new TrendModel();

            TimeWindow time;
            if (widgetConf.Time == "day")
            {
                time = TimeWindow.Day;
            } else
            {
                time = TimeWindow.Week;
            }

            try
            {
                if (widgetConf.Media == "movie")
                {
                    ViewData["Media"] = "Movies";

                    var response = await client.GetTrendingMoviesAsync(time);
                    result.Movie = response.Results;

                    foreach (var item in result.Movie)
                    {
                        item.PosterPath = "http://image.tmdb.org/t/p/w185" + item.PosterPath;
                    }
                } else
                {
                    ViewData["Media"] = "TV shows";

                    var response = await client.GetTrendingTvAsync(time);
                    result.Tv = response.Results;

                    foreach (var item in result.Tv)
                    {
                        item.PosterPath = "http://image.tmdb.org/t/p/w185" + item.PosterPath;
                    }
                }
            }
            catch (HttpRequestException)
            {
                ViewData["Error"] = "Error requesting data";
            }
            return View(result);
        }
    }
}
