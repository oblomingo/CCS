using System;
using System.Collections.Generic;
using System.Text;
using CCS.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CCS.Repository
{
	/// <summary>
	/// Class for migrations with explicitly defined connection string
	/// In cli run (in this folder):
	/// dotnet ef  migrations add YourMigrationName --context CCS.Repository.Infrastructure.Contexts.StationContext -o Infrastructure\Migrations
	/// dotnet ef database update
	/// </summary>
	public class StationContextFactory : IDesignTimeDbContextFactory<StationContext>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public StationContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<StationContext>();
			optionsBuilder.UseSqlite("Data Source=station.db");

			return new StationContext(optionsBuilder.Options);
		}
	}
}
