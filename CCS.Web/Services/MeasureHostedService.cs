using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using CSS.GPIO;
using CSS.GPIO.TemperatureSensors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.Web.Services
{
	internal class MeasureHostedService : IHostedService
	{
		private readonly ILogger _logger;

		public MeasureHostedService(IServiceProvider services,
			ILogger<MeasureHostedService> logger)
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
				//var measureRepository =
				//	scope.ServiceProvider
				//		.GetRequiredService<IMeasureRepository>();

				sensor.OnMeasure += Sensor_OnMeasure;
				sensor.Start();

				//while (!cancellationToken.IsCancellationRequested)
				//{
				//	try
				//	{
				//		Thread.Sleep(new TimeSpan(0, 0, 10));
				//		var measures = gpioManager.GetCurrentMeasures();
				//		foreach (var measure in measures)
				//		{
				//			var measureEntity = new Measure
				//			{
				//				Location = measure.Location,
				//				Temperature = measure.Temperature,
				//				Humidity = measure.Humidity,
				//				Time = measure.Time
				//			};
				//			Debug.WriteLine(measureEntity.ToString());
				//			measureRepository.InsertMeasure(measureEntity);
				//		}
				//	}
				//	catch (Exception e)
				//	{
				//		_logger.LogError(e, "Error on writing measure to database");
				//	}
				//}
			}
		}

		private void Sensor_OnMeasure(object sender, SensorDataReadEventArgs e)
		{
			_logger.LogInformation($"[{DateTime.Now.ToShortTimeString()}] TemperatureCelsius: {e.TemperatureCelsius}, HumidityPercentage: {e.HumidityPercentage}");
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
