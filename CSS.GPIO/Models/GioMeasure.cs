using System;
using CCS.Repository.Enums;

namespace CSS.GPIO.Models
{
	public class GioMeasure
	{
		public Locations Location { get; set; }
		public decimal Temperature { get; set; }
		public decimal Humidity { get; set; }
		public DateTime Time { get; set; }
	}
}
