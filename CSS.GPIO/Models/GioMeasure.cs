using System;
using CCS.Repository.Enums;

namespace CSS.GPIO.Models
{
	public class GioMeasure
	{
		public Locations Location { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public DateTime Time { get; set; }
	}
}
