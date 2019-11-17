using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DEV_dashboard_2019.Models;
using DEV_dashboard_2019.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using DEV_dashboard_2019.Factory;
using System.Net.Http;
using DEV_dashboard_2019.Models.WidgetConf;

namespace DEV_dashboard_2019.Controllers
{
    public class HomeController : Controller
    {
        private readonly WidgetConfigurationDbContext _configContext;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(WidgetConfigurationDbContext context, UserManager<IdentityUser> userManager)
        {
            _configContext = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string username)
        {
            var widgetConf = await this.GetViewModelAsync();

            return View(widgetConf);
        }

        public async Task<string> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        [Authorize]
        public IActionResult AddWidget()
        {
            return View();
        }

        public IActionResult CreateWeatherWidget()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWeatherWidget([Bind("CityName")] WeatherWidgetConf widgetConf)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(widgetConf.CityName))
            {
                try
                {
                    await ApiClientFactory.WeatherInstance.GetWeatherAsync(widgetConf.CityName);
                } catch (HttpRequestException)
                {
                    return View(widgetConf);
                }

                widgetConf.UserId = await this.GetUserIdAsync();

                _configContext.WeatherConf.Add(widgetConf);
                await _configContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(widgetConf);
        }

        public IActionResult CreateAchievementWidget()
        {
            ViewData["Error"] = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAchievementWidget([Bind("Username, AppId")] AchievementWidgetConf widgetConf)
        {
            ViewData["Error"] = "";
            if (!ModelState.IsValid || String.IsNullOrEmpty(widgetConf.Username))
            {
                ViewData["Error"] = "Invalid input";
                return View(widgetConf);
            }

            var response = await ApiClientFactory.SteamInstance.GetSteamIdAsync(widgetConf.Username);
            if (response.Response.Success == 42)
            {
                ViewData["Error"] = "No match";
                return View(widgetConf);
            }

            widgetConf.SteamId = response.Response.Id;

            widgetConf.UserId = await this.GetUserIdAsync();
            _configContext.AchievementConf.Add(widgetConf);

            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateFriendListWidget()
        {
            ViewData["Error"] = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFriendListWidget([Bind("Username")] FriendListWidgetConf widgetConf)
        {
            ViewData["Error"] = "";
            if (!ModelState.IsValid || String.IsNullOrEmpty(widgetConf.Username))
            {
                ViewData["Error"] = "Invalid input";
                return View(widgetConf);
            }

            var response = await ApiClientFactory.SteamInstance.GetSteamIdAsync(widgetConf.Username);
            if (response.Response.Success == 42)
            {
                ViewData["Error"] = "No match";
                return View(widgetConf);
            }

            widgetConf.SteamId = response.Response.Id;

            widgetConf.UserId = await this.GetUserIdAsync();
            _configContext.FriendListConf.Add(widgetConf);

            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMovieWidget()
        {
            ViewData["Error"] = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovieWidget([Bind("Search, Year")] MovieWidgetConf widgetConf)
        {
            ViewData["Error"] = "";
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Invalid input";
                return View(widgetConf);
            }

            widgetConf.UserId = await this.GetUserIdAsync();
            _configContext.MovieConf.Add(widgetConf);

            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateTrendWidget()
        {
            ViewData["Error"] = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrendWidget([Bind("Media, Time")] TrendWidgetConf widgetConf)
        {
            ViewData["Error"] = "";
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Invalid input";
                return View(widgetConf);
            }

            widgetConf.UserId = await this.GetUserIdAsync();
            _configContext.TrendConf.Add(widgetConf);

            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateDetailWidget()
        {
            ViewData["Error"] = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetailWidget([Bind("Name")] DetailWidgetConf widgetConf)
        {
            ViewData["Error"] = "";
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Invalid input";
                return View(widgetConf);
            }

            widgetConf.UserId = await this.GetUserIdAsync();
            _configContext.DetailConf.Add(widgetConf);

            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<WidgetViewModel> GetViewModelAsync()
        {
            var userId = await this.GetUserIdAsync();

            var widgetConf = new WidgetViewModel();

            if (_configContext.WeatherConf.Any())
            {
                var weather = from m in _configContext.WeatherConf
                              select m;
                weather = weather.Where(conf => conf.UserId.Equals(userId));
                widgetConf.WeatherConf = await weather.ToListAsync();
            }

            if (_configContext.AchievementConf.Any())
            {
                var achievement = from m in _configContext.AchievementConf
                                  select m;
                achievement = achievement.Where(conf => conf.UserId.Equals(userId));
                widgetConf.AchievementConf = await achievement.ToListAsync();
            }
            if (_configContext.FriendListConf.Any())
            {
                var friendList = from m in _configContext.FriendListConf
                                 select m;
                friendList = friendList.Where(conf => conf.UserId.Equals(userId));
                widgetConf.FriendListConf = await friendList.ToListAsync();
            }
            if (_configContext.MovieConf.Any())
            {
                var movie = from m in _configContext.MovieConf
                            select m;
                movie = movie.Where(conf => conf.UserId.Equals(userId));
                widgetConf.MovieConf = await movie.ToListAsync();
            }
            if (_configContext.TrendConf.Any())
            {
                var trend = from m in _configContext.TrendConf
                            select m;
                trend = trend.Where(conf => conf.UserId.Equals(userId));
                widgetConf.TrendConf = await trend.ToListAsync();
            }
            if (_configContext.DetailConf.Any())
            {
                var detail = from m in _configContext.DetailConf
                            select m;
                detail = detail.Where(conf => conf.UserId.Equals(userId));
                widgetConf.DetailConf = await detail.ToListAsync();
            }

            return widgetConf;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            var widget = await _configContext.WeatherConf.FindAsync(id);
            _configContext.WeatherConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFriendList(int id)
        {
            var widget = await _configContext.FriendListConf.FindAsync(id);
            _configContext.FriendListConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            var widget = await _configContext.AchievementConf.FindAsync(id);
            _configContext.AchievementConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var widget = await _configContext.MovieConf.FindAsync(id);
            _configContext.MovieConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            var widget = await _configContext.DetailConf.FindAsync(id);
            _configContext.DetailConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTrend(int id)
        {
            var widget = await _configContext.TrendConf.FindAsync(id);
            _configContext.TrendConf.Remove(widget);
            await _configContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
