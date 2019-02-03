using System;

namespace CSS.GPIO.TemperatureSensors
{
	public interface ITemperatureSensor : IDisposable
	{
		event EventHandler<SensorDataReadEventArgs> OnMeasure;
		void Start();
		void Dispose();
	}
}
