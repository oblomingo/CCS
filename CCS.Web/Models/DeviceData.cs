using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Enums;

namespace CCS.Web.Models
{
	public class DeviceData
	{
		//Sensor data
		public Locations Location { get; set; }
		public decimal Temperature { get; set; }
		public decimal Humidity { get; set; }
		public DateTime Time { get; set; }

		//Relay
		public bool isOn { get; set; }

		//Settings
		public Setting Settings { get; set; }
	}
}
