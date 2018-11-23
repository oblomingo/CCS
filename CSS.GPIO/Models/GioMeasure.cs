using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.GPIO.Models
{
    public class GioMeasure
    {
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Time { get; set; }
    }
}
