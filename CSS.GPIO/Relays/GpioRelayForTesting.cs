using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CSS.GPIO.Relays
{
	public class GpioRelayForTesting : IGpioRelay
	{
		private readonly ILogger _logger;

		public GpioRelayForTesting(ILogger<GpioRelayForTesting> logger)
		{
			_logger = logger;
		}

		private bool _isOn = false;
		public bool IsOn => _isOn;
		public void TurnOn()
		{
			_isOn = true;
			_logger.LogInformation($"GpioRelay turned on");
		}

		public void TurnOff()
		{
			_isOn = false;
			_logger.LogInformation($"GpioRelay turned off");
		}
	}
}
