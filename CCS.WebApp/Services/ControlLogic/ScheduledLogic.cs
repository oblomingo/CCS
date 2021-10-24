using System;
using CCS.Repository.Entities;
using CSS.GPIO.Relays;

namespace CCS.WebApp.Services.ControlLogic
{
    public class ScheduledLogic : IControlLogic

    {
        private readonly Setting _setting;
        private readonly IGpioRelay _gpioRelay;

        public ScheduledLogic(Setting setting, IGpioRelay gpioRelay)
        {
            _setting = setting;
            _gpioRelay = gpioRelay;
        }

        public void Apply()
        {
            throw new NotImplementedException();
        }

        public void Decline()
        {
            throw new NotImplementedException();
        }
    }
}
