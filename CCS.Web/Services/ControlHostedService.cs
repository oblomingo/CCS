using System;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Services.ControlLogic;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.Web.Services
{
	public class ControlHostedService : IHostedService
	{
		private readonly ILogger _logger;
		private IControlLogic _controlLogic;

		public ControlHostedService(IServiceProvider services, IBackgroundQueue queue, ILogger<ControlHostedService> logger)
		{
			Services = services;
			Queue = queue;
			_logger = logger;

			SetLogicControl();
		}

		public IBackgroundQueue Queue { get; }

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

				_controlLogic?.Decline();

				switch (setting.Mode)
				{
					case Modes.Manual:
						_controlLogic = new ManualLogic(setting, gpioRelay);
						break;
					case Modes.Automatic:
						_controlLogic = new AutomaticLogic(setting, gpioRelay, sensor);
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

		public IServiceProvider Services { get; }

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					var settings = await Queue.DequeueAsync(cancellationToken);
					await SetLogicControl(settings);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, $"Error occurred applying new settings.");
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
