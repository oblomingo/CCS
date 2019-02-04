using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.GPIO.Relays
{
	public interface IGpioRelay
	{
		bool IsOn { get; }
		void TurnOn();
		void TurnOff();
	}
}
