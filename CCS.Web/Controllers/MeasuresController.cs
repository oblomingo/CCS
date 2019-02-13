using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCS.Web.Controllers
{
	[Route("api/[controller]")]
	public class MeasuresController : Controller
	{
		private readonly IMeasureRepository _measureRepository;

		public MeasuresController(IMeasureRepository measureRepository)
		{
			_measureRepository = measureRepository;
		}

		[HttpGet]
		public async Task<List<MeasuresChartData>> Measures()
		{
			var measures = await _measureRepository.GetMeasuresByDates(DateTime.Now.AddDays(-1), DateTime.Now);
			
			List<MeasuresChartData> chartData = new List<MeasuresChartData>();

			foreach (var measure in measures)
			{
				MeasuresChartData data = new MeasuresChartData
				{
					Time = measure.Time.ToString("dd HH:mm"),
					Temperature = measure.Temperature,
					Humidity = measure.Humidity,
					IsOn = measure.IsOn ? 1 : 0
				};
				chartData.Add(data);
			}

			return chartData;
		}

		[HttpPost]
		public async void AddMeasure(Measure measure)
		{
			_measureRepository.InsertMeasure(measure);
		}
	}
}
