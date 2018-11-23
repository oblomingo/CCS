using CCS.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCS.Repository.Infrastructure.Configurations
{
	public class MeasureConfiguration
	{
		public MeasureConfiguration(EntityTypeBuilder<Measure> entityBuilder)
		{
			entityBuilder.ToTable("Measures");
			entityBuilder.HasKey(x => x.MeasureId);

			entityBuilder.Property(x => x.Location).IsRequired();
			entityBuilder.Property(x => x.Time).IsRequired();

			entityBuilder.Property(x => x.Temperature).IsRequired();
			entityBuilder.Property(x => x.Humidity).IsRequired();
		}
	}
}
