using DEV_dashboard_2019.Models.WidgetConf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace DEV_dashboard_2019.ViewComponents
{
    public class MovieWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MovieWidgetConf widgetConf)
        {
            var client = new TMDbClient("bcf08ea1238c643647ae3e34a0fd4d75");

            ViewData["Error"] = "";
            ViewData["Search"] = widgetConf.Search;
            ViewData["Id"] = widgetConf.Id;

            var error = new List<SearchMovie>();

            try
            {
                var response = await client.SearchMovieAsync(widgetConf.Search, 1, false, widgetConf.Year);

                foreach (var item in response.Results)
                {
                    item.PosterPath = "http://image.tmdb.org/t/p/w185" + item.PosterPath;
                }

                return View(response.Results);
            }
            catch (HttpRequestException)
            {
                ViewData["Error"] = "Error requesting movie data";
            }
            return View(error);
        }
    }
}
