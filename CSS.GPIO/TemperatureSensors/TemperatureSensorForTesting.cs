using System;
using System.Threading;
using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.TemperatureSensors
{
	public class TemperatureSensorForTesting : ITemperatureSensor
	{
		private readonly TimeSpan ReadInterval = TimeSpan.FromSeconds(2);
		private readonly Thread ReadWorker;

		public TemperatureSensorForTesting(P1 pin)
		{
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

		private void PerformContinuousReads()
		{
			while (IsRunning)
			{
				try
				{
					Thread.Sleep(ReadInterval);
					var sensorData = new SensorDataReadEventArgs(temperatureCelsius: 1, humidityPercentage: 1);
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