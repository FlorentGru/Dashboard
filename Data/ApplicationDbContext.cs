using System;
using System.Collections.Generic;
using System.Text;
using DEV_dashboard_2019.Models;
using DEV_dashboard_2019.Models.WidgetConf;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEV_dashboard_2019.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WeatherWidgetConf> WeatherConf { get; set; }

        public DbSet<AchievementWidgetConf> AchievementConf { get; set; }

        public DbSet<FriendListWidgetConf> FriendListConf { get; set; }

        public DbSet<MovieWidgetConf> MovieConf { get; set; }

        public DbSet<TrendWidgetConf> TrendConf { get; set; }

        public DbSet<DetailWidgetConf> DetailConf { get; set; }

    }
}
