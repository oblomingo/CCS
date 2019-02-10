using System;
using System.Reactive.Linq;
using CCS.Repository.Entities;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;

namespace CCS.Web.Services.ControlLogic
{
	public class AutomaticLogic : IControlLogic
	{
		private readonly Setting _setting;
		private readonly IGpioRelay _gpioRelay;
		private readonly ITemperatureSensor _temperatureSensor;
		private IDisposable _subscription;

		public AutomaticLogic(Setting setting, IGpioRelay gpioRelay, ITemperatureSensor temperatureSensor)
		{
			_setting = setting;
			_gpioRelay = gpioRelay;
			_temperatureSensor = temperatureSensor;
		}

		public void Apply()
		{
			_subscription?.Dispose();

			//Apply logic instantly
			Sensor_OnMeasure(new SensorDataReadEventArgs(_temperatureSensor.CurrentMeasure.Temperature, _temperatureSensor.CurrentMeasure.Humidity));

			//Apply logic periodically 
			_subscription = Observable
				.FromEventPattern<SensorDataReadEventArgs>(_temperatureSensor, "OnMeasure")
				.Sample(TimeSpan.FromMinutes(1))
				.Subscribe(x => Sensor_OnMeasure(x.EventArgs));
		}

		public void Decline()
		{
			_subscription?.Dispose();
		}

		private void Sensor_OnMeasure(SensorDataReadEventArgs e)
		{
			if (e.TemperatureCelsius < _setting.InnerTemperatureMin)
			{
				_gpioRelay.TurnOn();
			}
			else
			{
				_gpioRelay.TurnOff();
			}
		}
	}
}
