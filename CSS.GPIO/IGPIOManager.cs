using System.Collections.Generic;
using CSS.GPIO.Models;

namespace CSS.GPIO
{
	public interface IGpioManager
	{
		List<GioMeasure> GetCurrentMeasures();
		void ToggleRelay(bool turnOn);
	}
}
