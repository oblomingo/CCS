using System;
using CSS.GPIO.Models;

namespace CSS.GPIO.TemperatureSensors
{
	public interface ITemperatureSensor : IDisposable
	{
		GioMeasure CurrentMeasure { get; }
		event EventHandler<SensorDataReadEventArgs> OnMeasure;
		void Start();
		void Dispose();
	}
}
