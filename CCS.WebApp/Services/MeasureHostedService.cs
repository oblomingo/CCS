using System;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.WebApp.Settings;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCS.WebApp.Services
{
    internal class MeasureHostedService : IHostedService
    {
        private readonly ILogger _logger;

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

                sensor.OnMeasure += Sensor_OnMeasure1;
                sensor.Start();
            }
        }

        private void Sensor_OnMeasure1(object sender, SensorDataReadEventArgs e)
        {
            Console.WriteLine($"Measure sensor. Temperature: {e.TemperatureCelsius} C, humidity: {e.HumidityPercentage} %");
            using (var scope = Services.CreateScope())
            {
                var measureRepository = scope.ServiceProvider.GetRequiredService<IMeasureRepository>();
                var gpioRelay = scope.ServiceProvider.GetRequiredService<IGpioRelay>();

                try
                {
                    var measureEntity = new Measure
                    {
                        Location = Locations.Inside,
                        Temperature = e.TemperatureCelsius,
                        Humidity = e.HumidityPercentage,
                        IsOn = gpioRelay.IsOn,
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
            return Task.CompletedTask;
        }
    }
}
