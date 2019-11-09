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
                steamId = response.Response;
                if (steamId.Success != 1)
                {
                    return View(playerAchievementError);
                }

                if (_context.SteamId.Any()) {
                    _context.SteamId.Update(steamId);
                } else
                {
                    _context.SteamId.Add(steamId);
                }
                await _context.SaveChangesAsync();
            } else
            {
                if (_context.SteamId.Any())
                {
                    var id = _context.SteamId.First();
                    steamId = id;
                } else
                {
                    steamId.Id = "76561198175090246";
                }
            }

            if (String.IsNullOrEmpty(steamId.Id))
            {
                return View(playerAchievementError);
            }

            var webInterfaceFactory = new SteamWebInterfaceFactory("EE0B4484350CF5909C19342297A18ADB");
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUserStats>(new HttpClient());

            try
            {
                var playerAchievementsResponse = await steamInterface.GetPlayerAchievementsAsync(440, Convert.ToUInt64(steamId.Id));
                var playerAchievementsData = playerAchievementsResponse.Data;
                return View(playerAchievementsData);
            }
            catch (Exception)
            {

            }
            return View(playerAchievementError);
        }

        private IViewComponentResult NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
