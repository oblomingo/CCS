namespace CSS.GPIO.TemperatureSensors
{
	public class SensorDataReadEventArgs
	{
		public SensorDataReadEventArgs(double temperatureCelsius, double humidityPercentage)
		{
			TemperatureCelsius = temperatureCelsius;
			HumidityPercentage = humidityPercentage;
		}
		public double TemperatureCelsius { get; }
		public double HumidityPercentage { get; }
	}
}
