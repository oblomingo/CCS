using System;
using CCS.Repository.Enums;
using CSS.GPIO.Models;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;
using Unosquare.RaspberryIO.Peripherals;

namespace CSS.GPIO.TemperatureSensors
{
	public class TemperatureSensor : TemperatureSensorAM2302, ITemperatureSensor
	{
		private GioMeasure _currentMeasure = new GioMeasure();
		public TemperatureSensor(P1 pin) : base(Pi.Gpio[pin])
		{
			OnDataAvailable += TemperatureSensor_OnDataAvailable;
		}
		public TemperatureSensor(GpioPin dataPin) : base(dataPin)
		{
			OnDataAvailable += TemperatureSensor_OnDataAvailable;
		}

		public GioMeasure CurrentMeasure => _currentMeasure;

		private void TemperatureSensor_OnDataAvailable(object sender, TemperatureSensorAM2302.AM2302DataReadEventArgs e)
		{
			_currentMeasure = new GioMeasure
			{
				Temperature = e.TemperatureCelsius,
				Humidity = e.HumidityPercentage,
				Location = Locations.Inside,
				Time = DateTime.Now
			};
			OnMeasure?.Invoke(this, new SensorDataReadEventArgs(e.TemperatureCelsius, e.HumidityPercentage));
		}

		public event EventHandler<SensorDataReadEventArgs> OnMeasure;
	}
}
