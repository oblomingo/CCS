using System.Collections.Generic;
using CSS.GPIO.Models;

namespace CSS.GPIO
{
    public interface IGPIOManager
    {
        List<Measure> GetCurrentMeasures();
        void ToogleRelay(bool turnOn);
    }
}
