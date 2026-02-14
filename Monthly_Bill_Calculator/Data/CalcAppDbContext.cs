using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.DB_Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Utilities for Month Entity
            modelBuilder.Entity<Electricity>().HasData(
                new Electricity
                {
                    Id = 1,
                    Consumption = 120,
                    Price = 0.20m
                }
            );

            modelBuilder.Entity<ColdWater>().HasData(
                new ColdWater
                {
                    Id = 1,
                    Consumption = 5,
                    Price = 2.50m
                }
            );

            modelBuilder.Entity<HotWater>().HasData(
                new HotWater
                {
                    Id = 1,
                    Consumption = 3,
                    Price = 4.00m
                }
            );

            modelBuilder.Entity<NaturalGas>().HasData(
                new NaturalGas
                {
                    Id = 1,
                    Consumption = 50,
                    Price = 1.20m
                }
            );

            modelBuilder.Entity<Steam>().HasData(
                new Steam
                {
                    Id = 1,
                    Consumption = 10,
                    Price = 3.00m
                }
            );

            modelBuilder.Entity<CentralHeating>().HasData(
                new CentralHeating
                {
                    Id = 1,
                    Consumption = 100,
                    Price = 0.15m
                }
            );

            // Seed Months
            modelBuilder.Entity<Month>().HasData(
                new Month
                {
                    Id = 1,
                    Year = 2025,
                    MonthNumber = 11,
                    ElectricityId = 1,
                    ColdWaterId = 1,
                    HotWaterId = 1,
                    NaturalGasId = 1,
                    SteamId = 1,
                    CentralHeatingId = 1
                },
                new Month
                {
                    Id = 2,
                    Year = 2025,
                    MonthNumber = 12,
                    ElectricityId = null,
                    ColdWaterId = null,
                    HotWaterId = null,
                    NaturalGasId = null,
                    SteamId = null,
                    CentralHeatingId = null
                },
                new Month
                {
                    Id = 3,
                    Year = 2026,
                    MonthNumber = 1,
                    ElectricityId = null,
                    ColdWaterId = null,
                    HotWaterId = null,
                    NaturalGasId = null,
                    SteamId = null,
                    CentralHeatingId = null
                }
            );
        }
    }
}