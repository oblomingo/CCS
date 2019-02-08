using CCS.Repository.Entities;
using CCS.Repository.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCS.Repository.Infrastructure.Configurations
{
	public class SettingConfiguration
	{
		public SettingConfiguration(EntityTypeBuilder<Setting> entityBuilder)
		{
			entityBuilder.ToTable("Settings");
			entityBuilder.HasKey(x => x.SettingId);

			entityBuilder.Property(x => x.Mode).IsRequired();

			entityBuilder.HasData(new Setting {SettingId = 1, Mode = Modes.Automatic, InnerTemperatureMin = 3});
		}
	}
}
