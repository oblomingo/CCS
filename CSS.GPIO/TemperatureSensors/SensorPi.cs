using Unosquare.RaspberryIO.Gpio;

namespace CSS.GPIO.TemperatureSensors
{
	public class SensorPi
	{
		static SensorPi()
		{

		}

		public static GpioController Gpio => null;

		public static P1 Pin { get; set; }
	}
}
