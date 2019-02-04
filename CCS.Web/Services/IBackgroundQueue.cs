using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;

namespace CCS.Web.Services
{
	public interface IBackgroundQueue
	{
		void QueueBackgroundWorkItem(Setting setting);

		Task<Setting> DequeueAsync(CancellationToken cancellationToken);
	}
}
