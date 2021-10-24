using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCS.Repository.Enums;

namespace CCS.Repository.Entities
{
    public class Measure
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Key]
		public int MeasureId { get; set; }
        public Locations Location { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
		public bool IsOn { get; set; }
		public DateTime Time { get; set; }

	    public override string ToString() =>
		    $"Location: {Location}, Temperature: {Temperature}, Humidity: {Humidity}, Time: {Time.ToShortTimeString()}";
    }
}
