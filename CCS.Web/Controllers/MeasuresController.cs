using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Repositories;
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
		public async Task<List<Measure>> Measures()
		{
			var measures = await _measureRepository.GetMeasuresByDates(DateTime.Now.AddDays(-1), DateTime.Now);
			return measures;
		}

		[HttpPost]
		public async void AddMeasure(Measure measure)
		{
			_measureRepository.InsertMeasure(measure);
		}
	}
}
