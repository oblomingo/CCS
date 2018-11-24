using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using CSS.GPIO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.Web.Services
{
	internal class ConsumeScopedServiceHostedService : IHostedService
	{
		private readonly ILogger _logger;

		public ConsumeScopedServiceHostedService(IServiceProvider services,
			ILogger<ConsumeScopedServiceHostedService> logger)
		{
			Services = services;
			_logger = logger;
		}

		public IServiceProvider Services { get; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Consume Scoped Service Hosted Service is starting.");

			DoWork();

			return Task.CompletedTask;
		}

		private void DoWork()
		{
			_logger.LogInformation(
				"Consume Scoped Service Hosted Service is working.");

			using (var scope = Services.CreateScope())
			{
				var gpioManager =
					scope.ServiceProvider
						.GetRequiredService<IGpioManager>();
				var measureRepository =
					scope.ServiceProvider
						.GetRequiredService<IMeasureRepository>();

				while (true)
				{
					try
					{
						Thread.Sleep(new TimeSpan(0, 0, 10));
						var measures = gpioManager.GetCurrentMeasures();
						foreach (var measure in measures)
						{
							var measureEntity = new Measure
							{
								Location = measure.Location,
								Temperature = measure.Temperature,
								Humidity = measure.Humidity,
								Time = measure.Time
							};
							Debug.WriteLine(measureEntity.ToString());
							measureRepository.InsertMeasure(measureEntity);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Consume Scoped Service Hosted Service is stopping.");

			return Task.CompletedTask;
		}
	}
}
