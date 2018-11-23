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

	    [HttpGet, Route("Measures")]
	    public async Task<List<Measure>> Measures()
	    {
		    try
		    {
			    var measures = await _measureRepository.GetMeasuresByDates(DateTime.Now.AddDays(-1), DateTime.Now);
			    return measures;
			}
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }
	    }

	    [HttpPost, Route("Add")]
	    public async void AddMeasure(Measure measure)
	    {
		    try
		    {
			    List<Measure> measures = new List<Measure> { measure };
			    _measureRepository.InsertMeasures(measures);
			}
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }
	    }
	}
}
