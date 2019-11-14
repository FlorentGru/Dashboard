using DEV_dashboard_2019.Data;
using DEV_dashboard_2019.Factory;
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
    public class SteamViewComponent : ViewComponent
    {
        private readonly SteamIdContext _context;
        public SteamViewComponent(SteamIdContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username)
        {
            SteamId steamId = new SteamId();
            var playerAchievementError = new PlayerAchievementResultModel();
            playerAchievementError.Success = false;
            if (!String.IsNullOrEmpty(username))
            {
                var response = await SteamApiClientFactory.Instance.GetSteamId(username);
                if (response.Response.Success == 42)
                {
                    ViewData["Error"] = "No match";
                    return View(playerAchievementError);
                }

                if (_context.SteamId.Any()) {
                    steamId = _context.SteamId.First();
                    steamId.Id = response.Response.Id;
                    _context.SteamId.Update(steamId);
                } else
                {
                    steamId = response.Response;
                    _context.SteamId.Add(steamId);
                }
                await _context.SaveChangesAsync();
            } else
            {
                if (_context.SteamId.Any())
                {
                    steamId = _context.SteamId.First();
                } else
                {
                    steamId.Id = "";
                }
            }

            if (String.IsNullOrEmpty(steamId.Id))
            {
                ViewData["Error"] = "Please enter a steam ID username (username used in your account's steam community URL). Also the account must be public.";
                return View(playerAchievementError);
            }

            var webInterfaceFactory = new SteamWebInterfaceFactory("EE0B4484350CF5909C19342297A18ADB");
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUserStats>(new HttpClient());

            try
            {
                var playerAchievementsResponse = await steamInterface.GetPlayerAchievementsAsync(252950, Convert.ToUInt64(steamId.Id));
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
                } else
                {
                    ViewData["Error"] = "No stats for this account on this game.";
                }
            }
            return View(playerAchievementError);
        }
    }
}
