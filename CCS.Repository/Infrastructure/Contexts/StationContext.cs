using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CCS.Repository.Infrastructure.Contexts
{
    public class StationContext : DbContext
    {
	    public StationContext(DbContextOptions<StationContext> options)
		    : base(options)
	    { }

		public DbSet<Measure> Measures { get; set; }
        public DbSet<Setting> Settings { get; set; }

	    protected override void OnModelCreating(ModelBuilder modelBuilder)
	    {
		    base.OnModelCreating(modelBuilder);

		    new MeasureConfiguration(modelBuilder.Entity<Measure>());
		    new SettingConfiguration(modelBuilder.Entity<Setting>());
		}
	}
}
