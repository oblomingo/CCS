using System;
using System.Collections.Generic;
using System.Linq;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Contexts;
using EFCore.BulkExtensions;

namespace CCS.Repository.Infrastructure.Repositories
{
	public class MeasureRepository : IMeasureRepository
	{
		private readonly StationContext _stationContext;
		public MeasureRepository(StationContext stationContext)
		{
			_stationContext = stationContext;
		}

		public List<Measure> GetMeasuresByDates(DateTime start, DateTime end)
		{
			return _stationContext.Measures.Where(x => x.Time > start && x.Time < end).ToList();
		}

		public void InsertMeasures(List<Measure> measures)
		{
			_stationContext.BulkInsert(measures);
		}
	}
}
