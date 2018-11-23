using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Contexts;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CCS.Repository.Infrastructure.Repositories
{
	public class MeasureRepository : IMeasureRepository
	{
		private readonly StationContext _stationContext;
		public MeasureRepository(StationContext stationContext)
		{
			_stationContext = stationContext;
		}

		public async Task<List<Measure>> GetMeasuresByDates(DateTime start, DateTime end)
		{
			return await _stationContext.Measures.Where(x => x.Time > start && x.Time < end).ToListAsync();
		}

		public async void InsertMeasures(List<Measure> measures)
		{
			await _stationContext.BulkInsertAsync(measures);
		}
	}
}
