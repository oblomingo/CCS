using System;
using System.Threading;
using CCS.Repository.Enums;
using CSS.GPIO.Models;
using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.TemperatureSensors
{
	public class TemperatureSensorForTesting : ITemperatureSensor
	{
		private readonly TimeSpan ReadInterval = TimeSpan.FromSeconds(2);
		private readonly Thread ReadWorker;
		private readonly Random _random;
		private GioMeasure _currentMeasure = new GioMeasure();

		public TemperatureSensorForTesting(P1 pin)
		{
			_random = new Random();
			ReadWorker = new Thread(PerformContinuousReads);
		}

		public bool IsRunning { get; private set; }

		public event EventHandler<SensorDataReadEventArgs> OnMeasure;

		public void Start()
		{
			IsRunning = true;
			ReadWorker.Start();
		}

		public void Dispose()
		{
			if (IsRunning == false)
				return;

			StopContinuousReads();
		}

		public GioMeasure CurrentMeasure => _currentMeasure;

		private void PerformContinuousReads()
		{
			while (IsRunning)
			{
				try
				{
					Thread.Sleep(ReadInterval);
					var sensorData =
						new SensorDataReadEventArgs(
							temperatureCelsius: new decimal(_random.NextDouble()) * 10,
							humidityPercentage: new decimal(_random.NextDouble()) * 100);

					_currentMeasure = new GioMeasure
					{
						Temperature = sensorData.TemperatureCelsius,
						Humidity = sensorData.HumidityPercentage,
						Location = Locations.Inside,
						Time = DateTime.Now
					};

					OnMeasure?.Invoke(this, sensorData);
				}
				catch
				{
					// swallow
				}
			}
		}

		private void StopContinuousReads() =>
			IsRunning = false;


	}
}