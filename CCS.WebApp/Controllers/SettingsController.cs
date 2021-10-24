using System.Threading.Channels;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using CCS.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CCS.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ChannelWriter<Setting> _channelWriter;

        public SettingsController(ISettingRepository settingRepository, IBackgroundQueue backgroundQueue, Channel<Setting> channel)
        {
            _settingRepository = settingRepository;
            //Queue = backgroundQueue;
            _channelWriter = channel.Writer;
        }

        //public IBackgroundQueue Queue { get; }

        [HttpGet]
        public async Task<Setting> GetSetting() => await _settingRepository.GetCurrentSetting();

        [HttpPost]
        public async Task<Setting> UpdateSetting([FromBody] Setting setting)
        {
            await _settingRepository.UpdateCurrentSetting(setting);
            //Queue.QueueBackgroundWorkItem(setting);
            await _channelWriter.WriteAsync(setting);
            return setting;
        }
    }
}
