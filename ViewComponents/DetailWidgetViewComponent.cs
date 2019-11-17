using DEV_dashboard_2019.Models.WidgetConf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace DEV_dashboard_2019.ViewComponents
{
    public class DetailWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DetailWidgetConf widgetConf)
        {
            var client = new TMDbClient("bcf08ea1238c643647ae3e34a0fd4d75");

            ViewData["Error"] = "";
            ViewData["Id"] = widgetConf.Id;

            var error = new Movie();
            try
            {
                var response = await client.SearchMovieAsync(widgetConf.Name, 1, false);
                if (response.TotalResults == 0)
                {
                    return View(error);
                }

                var movie = response.Results.First().Id;

                foreach(var item in response.Results)
                {
                    if (item.Title.ToLower() == widgetConf.Name.ToLower())
                    {
                        movie = item.Id;
                    }
                }

                var result = await client.GetMovieAsync(movie);

                result.BackdropPath = "http://image.tmdb.org/t/p/w780" + result.BackdropPath;

                return View(result);
            }
            catch (HttpRequestException)
            {
                ViewData["Error"] = widgetConf.Name + " is unknown movie";
            }
            return View(error);
        }
    }
}
