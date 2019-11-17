using DEV_dashboard_2019.Models.WidgetConf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel()
        {
            WeatherConf = new List<WeatherWidgetConf>();
            AchievementConf = new List<AchievementWidgetConf>();
            FriendListConf = new List<FriendListWidgetConf>();
            MovieConf = new List<MovieWidgetConf>();
            TrendConf = new List<TrendWidgetConf>();
            DetailConf = new List<DetailWidgetConf>();
        }

        public IEnumerable<WeatherWidgetConf> WeatherConf;

        public IEnumerable<AchievementWidgetConf> AchievementConf;

        public IEnumerable<FriendListWidgetConf> FriendListConf;

        public IEnumerable<MovieWidgetConf> MovieConf;

        public IEnumerable<TrendWidgetConf> TrendConf;

        public IEnumerable<DetailWidgetConf> DetailConf;
    }
}
