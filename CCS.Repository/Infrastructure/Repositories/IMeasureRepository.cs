using System;
using System.Collections.Generic;
using CCS.Repository.Entities;

namespace CCS.Repository.Infrastructure.Repositories
{
	public interface IMeasureRepository
	{
		List<Measure> GetMeasuresByDates(DateTime start, DateTime end);
	}
}
