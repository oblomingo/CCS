using System;
using CCS.Repository.Entities;
using CCS.Repository.Enums;

namespace CCS.WebApp.Models
{
    public class DeviceData
    {
        //Sensor data
        public Locations Location { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Time { get; set; }

        //Relay
        public bool isOn { get; set; }

        //Settings
        public Setting Settings { get; set; }
    }
}
