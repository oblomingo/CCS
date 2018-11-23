using System.Collections.Generic;
using CSS.GPIO;
using CSS.GPIO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCS.Web.Controllers
{
    [Route("api/[controller]")]
    public class GpioController : Controller
    {
        private readonly IGpioManager _gpioManager;

        public GpioController(IGpioManager gpioManager)
        {
            _gpioManager = gpioManager;
        }

        [HttpGet]
        public List<Measure> Measures() => _gpioManager.GetCurrentMeasures();
    }
}
