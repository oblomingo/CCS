using System;
using System.Collections.Generic;
using System.Text;
using CCS.Repository.Enums;

namespace CCS.Repository.Entities
{
    public class Measure
    {
        public int MeasureId { get; set; }
        public Locations Location { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
		public DateTime Time { get; set; }
    }
}
