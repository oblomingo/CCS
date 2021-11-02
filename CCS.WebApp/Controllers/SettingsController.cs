using System.Threading.Channels;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CCS.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ChannelWriter<Setting> _channelWriter;

        public SettingsController(ISettingRepository settingRepository, Channel<Setting> channel)
        {
            _settingRepository = settingRepository;
            _channelWriter = channel.Writer;
        }

        [HttpGet]
        public async Task<Setting> GetSetting() => await _settingRepository.GetCurrentSetting();

        [HttpPost]
        public async Task<Setting> UpdateSetting([FromBody] Setting setting)
        {
            await _settingRepository.UpdateCurrentSetting(setting);
            await _channelWriter.WriteAsync(setting);
            return setting;
        }
    }
}
