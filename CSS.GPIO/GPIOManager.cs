using System;
using System.Collections.Generic;
using CSS.GPIO.Models;

namespace CSS.GPIO
{
    public class GpioManager : IGpioManager
    {
        private bool _turnedOn;
        public List<Measure> GetCurrentMeasures()
        {
            //TODO Implement real measures getting
            List<Measure> measures = new List<Measure>();
            var rand = new Random();

            Measure measure = new Measure
            {
                Temperature = new decimal(rand.NextDouble()) * 10,
                Humidity = new decimal(rand.NextDouble()) * 100,
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
