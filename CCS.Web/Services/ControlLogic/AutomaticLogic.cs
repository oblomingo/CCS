using System;
using System.Diagnostics;
using CCS.Repository.Entities;
using CSS.GPIO;

namespace CCS.Web.Services.ControlLogic
{
	public class AutomaticLogic : IControlLogic
	{
		private readonly Setting _setting;

		public AutomaticLogic(Setting setting)
		{
			_setting = setting;
		}
		public bool ShouldBeOn()
		{
			Debug.WriteLine("Applying automatic mode logic ...");
			return true;
		}
	}
}
