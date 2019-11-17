using DEV_dashboard_2019.Models;
using Microsoft.AspNetCore.Mvc;
using Steam.Models.SteamCommunity;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.ViewComponents
{
    public class FriendListWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(FriendListWidgetConf conf)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory("EE0B4484350CF5909C19342297A18ADB");
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());

            ViewData["Username"] = conf.Username;
            ViewData["Id"] = conf.Id;

            var playerGamesError = new Collection<PlayerSummaryModel>();

            try
            {
                var friendListResponse = await steamInterface.GetFriendsListAsync(Convert.ToUInt64(conf.SteamId));
                var friendListData = friendListResponse.Data;

                var friendList = new List<ulong>();

                foreach (var item in friendListData)
                {
                    friendList.Add(item.SteamId);
                }

                var summaries = await steamInterface.GetPlayerSummariesAsync(friendList);

                return View(summaries.Data);
            }
            catch (HttpRequestException e)
            {
                if (e.HResult == 403)
                {
                    ViewData["Error"] = "The account must be public.";
                }
                else
                {
                    ViewData["Error"] = "No stats for this account";
                }
            }
            return View(playerGamesError);
        }
    }
}
