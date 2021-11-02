using Microsoft.Extensions.Logging;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace CSS.GPIO.Relays
{
    public class GpioRelay : IGpioRelay
	{
		private readonly ILogger _logger;
		private bool _isOn;
		public GpioRelay(ILogger<GpioRelayForTesting> logger)
		{
			_logger = logger;

			Pi.Gpio[12].PinMode = GpioPinDriveMode.Input;
			_isOn = Pi.Gpio[12].Read();
		}

		public bool IsOn => _isOn;

		public void TurnOn()
		{
			if (IsOn)
			{
				return;
			}

			Pi.Gpio[12].PinMode = GpioPinDriveMode.Output;
			Pi.Gpio[12].Write(GpioPinValue.High);
			_isOn = true;
			_logger.LogInformation($"GpioRelay turned on");
		}

		public void TurnOff()
		{
			if (IsOn)
			{
				Pi.Gpio[12].PinMode = GpioPinDriveMode.Output;
				Pi.Gpio[12].Write(GpioPinValue.Low);
				_isOn = false;
				_logger.LogInformation($"GpioRelay turned off");
			}
		}
	}
}
