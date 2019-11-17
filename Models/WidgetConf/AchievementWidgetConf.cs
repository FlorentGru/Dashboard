using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    public class AchievementWidgetConf
    {

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Steam username")]
        public string Username { get; set; }

        public string SteamId { get; set; }

        [Display(Name = "Game Id")]
        public int AppId { get; set; }
    }
}
