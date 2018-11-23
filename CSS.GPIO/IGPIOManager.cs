using System.Collections.Generic;
using CSS.GPIO.Models;

namespace CSS.GPIO
{
    public interface IGpioManager
    {
        List<Measure> GetCurrentMeasures();
        void ToggleRelay(bool turnOn);
    }
}
