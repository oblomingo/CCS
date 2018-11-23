using CCS.Repository.Entities;

namespace CCS.Repository.Infrastructure.Repositories
{
	public interface ISettingRepository
	{
		Setting GetCurrentSetting();
		Setting UpdateCurrentSetting(Setting setting);
	}
}
