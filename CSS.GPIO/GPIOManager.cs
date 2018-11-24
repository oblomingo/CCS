using System;
using System.Collections.Generic;
using CCS.Repository.Enums;
using CSS.GPIO.Models;

namespace CSS.GPIO
{
	public class GpioManager : IGpioManager
	{
		private bool _turnedOn;
		private readonly Random _random;

		public GpioManager()
		{
			_random = new Random();
		}

		public List<GioMeasure> GetCurrentMeasures()
		{
			//TODO Implement real measures getting
			List<GioMeasure> measures = new List<GioMeasure>();

			GioMeasure measure = new GioMeasure
			{
				Location = Locations.Outside,
				Temperature = new decimal(_random.NextDouble()) * 10,
				Humidity = new decimal(_random.NextDouble()) * 100,
				Time = DateTime.Now
			};
			measures.Add(measure);

			return measures;
		}

		public void ToggleRelay(bool turnOn)
		{
			_turnedOn = turnOn;
		}
	}
}
