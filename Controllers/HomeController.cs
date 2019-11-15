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
        public IActionResult Index(string username)
        {

            ViewData["username"] = username;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Widget()
        {
            var widgetConf = await this.GetViewModelAsync();

            return View(widgetConf);
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
            return widgetConf;
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
                widgetConf.UserId = await this.GetUserIdAsync();

                _configContext.WeatherConf.Add(widgetConf);
                await _configContext.SaveChangesAsync();

                return RedirectToAction(nameof(Widget));
            }
            return View(widgetConf);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
