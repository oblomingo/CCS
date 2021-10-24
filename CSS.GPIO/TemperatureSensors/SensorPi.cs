using Unosquare.RaspberryIO.Abstractions;

namespace CSS.GPIO.TemperatureSensors
{
	public class SensorPi
	{
		static SensorPi()
		{

		}

		public static IGpioController Gpio => null;

		public static P1 Pin { get; set; }
	}
}
