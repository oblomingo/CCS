using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CCS.Repository.Entities;

namespace CCS.Repository.Infrastructure.Repositories
{
	public interface IMeasureRepository
	{
		Task<List<Measure>> GetMeasuresByDates(DateTime start, DateTime end);
		void InsertMeasures(List<Measure> measures);
	}
}
