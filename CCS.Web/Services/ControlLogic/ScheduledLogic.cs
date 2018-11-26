using System;
using System.Diagnostics;
using CCS.Repository.Entities;

namespace CCS.Web.Services.ControlLogic
{
	public class ScheduledLogic : IControlLogic
	{
		private readonly Setting _setting;

		public ScheduledLogic(Setting setting)
		{
			_setting = setting;
		}
		public bool ShouldBeOn()
		{
			Debug.WriteLine("Applying scheduled mode logic ...");
			return false;
		}
	}
}
