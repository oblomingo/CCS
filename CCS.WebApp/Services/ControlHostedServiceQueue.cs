using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;

namespace CCS.WebApp.Services
{
    public class ControlHostedServiceQueue : IBackgroundQueue
    {
        private ConcurrentQueue<Setting> _workItems =
            new ConcurrentQueue<Setting>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Setting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            _workItems.Enqueue(setting);
            _signal.Release();
        }

        public async Task<Setting> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
