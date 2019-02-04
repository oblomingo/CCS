﻿using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CCS.Web.Controllers
{
	public class SettingsController : Controller
	{
		private readonly ISettingRepository _settingRepository;

		public SettingsController(ISettingRepository settingRepository, IBackgroundQueue backgroundQueue)
		{
			_settingRepository = settingRepository;
			Queue = backgroundQueue;
		}

		public IBackgroundQueue Queue { get; }

		[HttpGet, Route("CurrentSetting")]
		public async Task<Setting> GetSetting() => await _settingRepository.GetCurrentSetting();

		[HttpPost, Route("CurrentSetting")]
		public async Task<Setting> UpdateSetting(Setting setting)
		{
			await _settingRepository.UpdateCurrentSetting(setting);
			Queue.QueueBackgroundWorkItem(setting);
			return setting;
		}
	}
}
