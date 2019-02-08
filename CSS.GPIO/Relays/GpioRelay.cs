using System;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.Relays
{
	public class GpioRelay : IGpioRelay
	{
		private readonly ILogger _logger;
		public GpioRelay(ILogger<GpioRelayForTesting> logger)
		{
			_logger = logger;
		}

		public bool IsOn
		{
			get
			{
				try
				{
					Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Input;
					return Pi.Gpio.Pin26.Read();
				}
				catch (Exception e)
				{
					_logger.LogError(e, "Gpio pin read error");
					return false;
				}
				
			}
		} 
		public void TurnOn()
		{
			if (IsOn)
			{
				return;
			}

			Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Output;
			Pi.Gpio.Pin26.Write(GpioPinValue.High);
			_logger.LogInformation($"GpioRelay turned on");
		}

		public void TurnOff()
		{
			if (IsOn)
			{
				Pi.Gpio.Pin26.PinMode = GpioPinDriveMode.Input;
				Pi.Gpio.Pin26.Write(GpioPinValue.Low);
				_logger.LogInformation($"GpioRelay turned off");
			}
		}
	}
}
