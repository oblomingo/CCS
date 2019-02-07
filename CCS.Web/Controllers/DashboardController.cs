using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CCS.Repository.Enums;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Models;
using CSS.GPIO;
using CSS.GPIO.Models;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.AspNetCore.Mvc;

namespace CCS.Web.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IGpioRelay _gpioRelay;
	    private readonly ITemperatureSensor _temperatureSensor;
	    private readonly ISettingRepository _settingRepository;

		public DashboardController(ITemperatureSensor temperatureSensor, IGpioRelay gpioRelay, ISettingRepository settingRepository)
        {
	        _temperatureSensor = temperatureSensor;
	        _gpioRelay = gpioRelay;
	        _settingRepository = settingRepository;
        }

	    [HttpGet]
	    public async Task<DeviceData> CurrentMeasures()
	    {
		    DeviceData data = new DeviceData
		    {
			    Temperature = _temperatureSensor.CurrentMeasure.Temperature,
			    Humidity = _temperatureSensor.CurrentMeasure.Humidity,
			    Location = _temperatureSensor.CurrentMeasure.Location,
			    Time = _temperatureSensor.CurrentMeasure.Time,
			    isOn = _gpioRelay.IsOn,
			    Settings = await _settingRepository.GetCurrentSetting()
		    };

		    return data;
	    }
	}
}
