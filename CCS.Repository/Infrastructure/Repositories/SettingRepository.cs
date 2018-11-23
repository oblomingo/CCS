using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CCS.Repository.Infrastructure.Repositories
{
	public class SettingRepository : ISettingRepository
	{
		private readonly StationContext _stationContext;

		public SettingRepository(StationContext stationContext)
		{
			_stationContext = stationContext;
		}

		public async Task<Setting> GetCurrentSetting()
		{
			var setting = await _stationContext.Settings.FirstOrDefaultAsync();
			if (setting == null)
			{
				return await Task.FromResult(new Setting());
			}

			return setting;
		}

		public async Task<Setting> UpdateCurrentSetting(Setting setting)
		{
			if (setting.SettingId > 0)
			{
				_stationContext.Update(setting);
			}
			else
			{
				_stationContext.Settings.Add(setting);
			}

			await _stationContext.SaveChangesAsync();

			return await Task.FromResult(setting); ;
		}
	}
}
