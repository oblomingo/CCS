using System;
using CCS.Repository.Enums;
using CSS.GPIO.Models;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;

namespace CSS.GPIO.TemperatureSensors
{
	public class TemperatureSensor : Dht22, ITemperatureSensor
	{
		private GioMeasure _currentMeasure = new GioMeasure();
		//public TemperatureSensor(P1 pin) : base(Pi.Gpio[pin])
		//{
		//	OnDataAvailable += TemperatureSensor_OnDataAvailable;
		//}

		public TemperatureSensor(IGpioPin dataPin) : base(dataPin)
		{
			OnDataAvailable += TemperatureSensor_OnDataAvailable;
		}

		public GioMeasure CurrentMeasure => _currentMeasure;

		private void TemperatureSensor_OnDataAvailable(object sender, DhtReadEventArgs e)
		{
			Console.WriteLine($"Temperature sensor data. Temperature: {e.Temperature} C, Humidity: {e.HumidityPercentage} %");
			_currentMeasure = new GioMeasure
			{
				Temperature = e.Temperature,
				Humidity = e.HumidityPercentage,
				Location = Locations.Inside,
				Time = DateTime.Now
			};
			OnMeasure?.Invoke(this, new SensorDataReadEventArgs(e.Temperature, e.HumidityPercentage));
		}

		public event EventHandler<SensorDataReadEventArgs> OnMeasure;
	}
}
