using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Services.ControlLogic;
using CSS.GPIO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.Web.Services
{
	internal class ControlHostedService : IHostedService
	{
		private readonly ILogger _logger;
		private IControlLogic _controlLogic;

		public ControlHostedService(IServiceProvider services, ILogger<ControlHostedService> logger)
		{
			Services = services;
			_logger = logger;
			SetLogicControl();
		}

		private async void SetLogicControl()
		{
			using (var scope = Services.CreateScope())
			{
				var settingRepository = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
				var currentSetting = await settingRepository.GetCurrentSetting();

				switch (currentSetting.Mode)
				{
					case Modes.Manual:
						_controlLogic = new ManualLogic(currentSetting);
						break;
					case Modes.Automatic:
						_controlLogic = new AutomaticLogic(currentSetting);
						break;
					case Modes.Schedule:
						_controlLogic = new ScheduledLogic(currentSetting);
						break;
					default:
						_controlLogic = new ManualLogic(currentSetting);
						break;
				}
			}
		}

		public IServiceProvider Services { get; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			DoWork(cancellationToken);
			return Task.CompletedTask;
		}
		
		private async void DoWork(CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Control hosted service is working...");

			using (var scope = Services.CreateScope())
			{
				var gpioManager =
					scope.ServiceProvider
						.GetRequiredService<IGpioManager>();
				while (!cancellationToken.IsCancellationRequested)
				{
					try
					{
						Thread.Sleep(new TimeSpan(0, 0, 10));
						Debug.WriteLine($"ControlHostedService");
						var shouldBeOn = _controlLogic.ShouldBeOn();
						gpioManager.ToggleRelay(shouldBeOn);
					}
					catch (Exception e)
					{
						_logger.LogError(e, "Error on writing measure to database");
					}
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
