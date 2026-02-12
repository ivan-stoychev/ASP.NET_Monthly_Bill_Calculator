using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.DB_Models;
using System.Collections.Generic;
using System.IO;

namespace Monthly_Bill_Calculator.Data
{
    public class MBCalcAppDbContext : DbContext
    {
        public MBCalcAppDbContext(DbContextOptions<MBCalcAppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Electricity> Electricity { get; set; }
        public DbSet<Steam> Steam { get; set; }
        public DbSet<ColdWater> ColdWater { get; set; }
        public DbSet<HotWater> HotWater { get; set; }
        public DbSet<NaturalGas> NaturalGas { get; set; }
        public DbSet<CentralHeating> CentralHeating { get; set; }
        public DbSet<Month> Months { get; set; }

    }
}