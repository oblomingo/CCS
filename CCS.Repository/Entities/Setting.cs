using System;
using CCS.Repository.Enums;

namespace CCS.Repository.Entities
{
	public class Setting
	{
		public int InnerTemperatureMin { get; set; }
		public int InnerTemperatureMax { get; set; }
		public int OuterTemperatureMin { get; set; }
		public int OuterTemperatureMax { get; set; }
		public Modes Mode { get; set; }
		public DateTime ScheduleStar { get; set; }
		public DateTime ScheduleStop { get; set; }
	}
}
