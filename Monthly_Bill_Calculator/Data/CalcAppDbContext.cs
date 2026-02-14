using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.DB_Models;
using System.Collections.Generic;
using System.IO;

namespace Monthly_Bill_Calculator.Data
{
    public class CalcAppDbContext : DbContext
    {
        public CalcAppDbContext(DbContextOptions<CalcAppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Electricity> Electricities { get; set; }
        public DbSet<Steam> Steams { get; set; }
        public DbSet<ColdWater> ColdWaters { get; set; }
        public DbSet<HotWater> HotWaters { get; set; }
        public DbSet<NaturalGas> NaturalGases { get; set; }
        public DbSet<CentralHeating> CentralHeatings { get; set; }
        public DbSet<Month> Months { get; set; }

    }
}