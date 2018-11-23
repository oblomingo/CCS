using System;
using System.Collections.Generic;
using CCS.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CCS.Repository
{
    public class StationContext : DbContext
    {
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=station.db");
        }
    }
}
