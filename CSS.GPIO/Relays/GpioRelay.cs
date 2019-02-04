using System;
using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.Relays
{
	public class GpioRelay : IGpioRelay
	{
		private readonly GpioController _gpioController;
		private readonly ILogger _logger;
		public GpioRelay(ILogger<GpioRelayForTesting> logger)
		{
			_gpioController = GpioController.Instance;
			_logger = logger;
		}
		public bool IsOn => _gpioController.Pin21.Read();
		public void TurnOn()
		{
			if (IsOn)
			{
				return;
			}

			_gpioController.Pin21.Write(GpioPinValue.High);
			_logger.LogInformation($"GpioRelay turned on");
		}

		public void TurnOff()
		{
			if (IsOn)
			{
				_gpioController.Pin21.Write(GpioPinValue.Low);
				_logger.LogInformation($"GpioRelay turned off");
			}
		}
	}
}
