using DEV_dashboard_2019.Models;
using Microsoft.AspNetCore.Mvc;
using Steam.Models.SteamPlayer;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.ViewComponents
{
    public class AchievementWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AchievementWidgetConf conf)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory("EE0B4484350CF5909C19342297A18ADB");
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUserStats>(new HttpClient());

            ViewData["Id"] = conf.Id;
            ViewData["Username"] = conf.Username;

            var playerAchievementError = new PlayerAchievementResultModel
            {
                Success = false
            };

            try
            {
                var playerAchievementsResponse = await steamInterface.GetPlayerAchievementsAsync(Convert.ToUInt32(conf.AppId), Convert.ToUInt64(conf.SteamId));
                var playerAchievementsData = playerAchievementsResponse.Data;

                if (playerAchievementsData.Success == false)
                {
                    ViewData["Error"] = "No stats for this account on this game";
                }

                return View(playerAchievementsData);
            }
            catch (HttpRequestException e)
            {
                if (e.HResult == 403)
                {
                    ViewData["Error"] = "The account must be public.";
                }
                else
                {
                    ViewData["Error"] = "No stats for this account on this game.";
                }
            }
            return View(playerAchievementError);
        }
    }
}