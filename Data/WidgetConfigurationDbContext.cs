using DEV_dashboard_2019.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Data
{
    public class WidgetConfigurationDbContext : DbContext
    {
        public WidgetConfigurationDbContext(DbContextOptions<WidgetConfigurationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherWidgetConf> WeatherConf { get; set; }
    }
}
