using System;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.Relays
{
	public class GpioRelay : IGpioRelay
	{
		private readonly ILogger _logger;
		private bool _isOn;
		public GpioRelay(ILogger<GpioRelayForTesting> logger)
		{
			_logger = logger;

			Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Input;
			_isOn = Pi.Gpio.Pin26.Read();
		}

		public bool IsOn => _isOn;

		public void TurnOn()
		{
			if (IsOn)
			{
				return;
			}

			Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Output;
			Pi.Gpio.Pin26.Write(GpioPinValue.High);
			_isOn = true;
			_logger.LogInformation($"GpioRelay turned on");
		}

		public void TurnOff()
		{
			if (IsOn)
			{
				Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Output;
				Pi.Gpio.Pin26.Write(GpioPinValue.Low);
				_isOn = false;
				_logger.LogInformation($"GpioRelay turned off");
			}
		}
	}
}
