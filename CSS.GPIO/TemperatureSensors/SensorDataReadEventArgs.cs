namespace CSS.GPIO.TemperatureSensors
{
	public class SensorDataReadEventArgs
	{
		public SensorDataReadEventArgs(decimal temperatureCelsius, decimal humidityPercentage)
		{
			TemperatureCelsius = temperatureCelsius;
			HumidityPercentage = humidityPercentage;
		}
		public decimal TemperatureCelsius { get; }
		public decimal HumidityPercentage { get; }
	}
}
