using System.Linq;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Contexts;

namespace CCS.Repository.Infrastructure.Repositories
{
	public class SettingRepository : ISettingRepository
	{
		private readonly StationContext _stationContext;

		public SettingRepository(StationContext stationContext)
		{
			_stationContext = stationContext;
		}

		public Setting GetCurrentSetting()
		{
			var setting = _stationContext.Settings.FirstOrDefault();
			return setting ?? new Setting();
		}

		public Setting UpdateCurrentSetting(Setting setting)
		{
			if (setting.SettingId > 0)
			{
				_stationContext.Update(setting);
			}
			else
			{
				_stationContext.Settings.Add(setting);
			}

			_stationContext.SaveChanges();

			return setting;
		}
	}
}
