using System.Threading.Tasks;
using CCS.Repository.Entities;

namespace CCS.Repository.Infrastructure.Repositories
{
	public interface ISettingRepository
	{
		Task<Setting> GetCurrentSetting();
		Task<Setting> UpdateCurrentSetting(Setting setting);
	}
}
