using DEV_dashboard_2019.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Data
{
    public class SteamIdContext : DbContext
    {
        public SteamIdContext(DbContextOptions<SteamIdContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<SteamId> SteamId { get; set; }

    }
}
