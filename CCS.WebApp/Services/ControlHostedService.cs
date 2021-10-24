using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.WebApp.Services.ControlLogic;
using CCS.WebApp.Settings;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.WebApp.Services
{
    public class ControlHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private IControlLogic _controlLogic;
        private readonly ChannelReader<Setting> _channelReader;

        public ControlHostedService(IServiceProvider services, ILogger<ControlHostedService> logger, Channel<Setting> channel)
        {
            Services = services;
            _logger = logger;
            _channelReader = channel.Reader;

            SetLogicControl();
        }

        public IServiceProvider Services { get; }

        public async Task SetLogicControl(Setting setting = null)
        {
            using (var scope = Services.CreateScope())
            {
                if (setting == null)
                {
                    var settingRepository = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
                    setting = await settingRepository.GetCurrentSetting();
                }

                var gpioRelay = scope.ServiceProvider.GetRequiredService<IGpioRelay>();
                var sensor = scope.ServiceProvider.GetRequiredService<ITemperatureSensor>();
                var gpioSettings = scope.ServiceProvider.GetRequiredService<GpioSettings>();

                _controlLogic?.Decline();

                switch (setting.Mode)
                {
                    case Modes.Manual:
                        _controlLogic = new ManualLogic(setting, gpioRelay);
                        break;
                    case Modes.Automatic:
                        _controlLogic = new AutomaticLogic(setting, gpioRelay, sensor, gpioSettings);
                        break;
                    case Modes.Schedule:
                        _controlLogic = new ScheduledLogic(setting, gpioRelay);
                        break;
                    default:
                        _controlLogic = new ManualLogic(setting, gpioRelay);
                        break;
                }

                _controlLogic.Apply();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await foreach (var setting in _channelReader.ReadAllAsync(cancellationToken))
            { 
                try
                {
                    await SetLogicControl(setting);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred applying new settings.");
                }
            }
        }
    }
}
