using CCS.Repository.Entities;
using CCS.Repository.Enums;

namespace CCS.Web.Services.ControlLogic
{
	public class ManualLogic : IControlLogic
	{
		private readonly Setting _setting;

		public ManualLogic(Setting setting)
		{
			_setting = setting;
		}

		public bool ShouldBeOn() => _setting.Mode == Modes.Manual && _setting.IsOn;
	}
}
