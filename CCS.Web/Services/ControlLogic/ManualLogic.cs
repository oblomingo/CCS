using CCS.Repository.Entities;
using CCS.Repository.Enums;
using CSS.GPIO.Relays;

namespace CCS.Web.Services.ControlLogic
{
	public class ManualLogic : IControlLogic
	{
		private readonly Setting _setting;
		private readonly IGpioRelay _gpioRelay;

		public ManualLogic(Setting setting, IGpioRelay gpioRelay)
		{
			_setting = setting;
			_gpioRelay = gpioRelay;
		}

		public void Apply()
		{
			if (ShouldBeOn)
			{
				_gpioRelay.TurnOn();
			}
			else
			{
				_gpioRelay.TurnOff();
			}
		}

		public void Decline()
		{
		}

		private bool ShouldBeOn => _setting.Mode == Modes.Manual && _setting.IsOn;
	}
}
