using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Settings;
using CSS.GPIO.TemperatureSensors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.Web.Services
{
	internal class MeasureHostedService : IHostedService
	{
		private readonly ILogger _logger;
		private IDisposable _subscription;

		public MeasureHostedService(IServiceProvider services, ILogger<MeasureHostedService> logger)
		{
			Services = services;
			_logger = logger;
		}

		public IServiceProvider Services { get; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			DoWork(cancellationToken);
			return Task.CompletedTask;
		}

		private void DoWork(CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Measure hosted service is working...");

			using (var scope = Services.CreateScope())
			{
				var sensor =
					scope.ServiceProvider
						.GetRequiredService<ITemperatureSensor>();

				var gpioSettings = scope.ServiceProvider.GetRequiredService<GpioSettings>();

				_subscription = Observable
					.FromEventPattern<SensorDataReadEventArgs>(sensor, "OnMeasure")
					.Sample(TimeSpan.FromSeconds(gpioSettings.MeasureIntervalInSeconds))
					.Subscribe(x => Sensor_OnMeasure(x.EventArgs));

				sensor.Start();
			}
		}

		private void Sensor_OnMeasure(SensorDataReadEventArgs e)
		{
			using (var scope = Services.CreateScope())
			{
				var measureRepository =
					scope.ServiceProvider
						.GetRequiredService<IMeasureRepository>();

				try
				{
					var measureEntity = new Measure
					{
						Location = Locations.Inside,
						Temperature = e.TemperatureCelsius,
						Humidity = e.HumidityPercentage,
						Time = DateTime.Now
					};

					measureRepository.InsertMeasure(measureEntity);
				}
				catch (Exception exception)
				{
					_logger.LogError(exception, "Error on saving measure to the database");
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_subscription?.Dispose();
			return Task.CompletedTask;
		}
	}
}
