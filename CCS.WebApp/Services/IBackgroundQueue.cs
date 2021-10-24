using System.Threading;
using System.Threading.Tasks;
using CCS.Repository.Entities;

namespace CCS.WebApp.Services
{
    public interface IBackgroundQueue
    {
        void QueueBackgroundWorkItem(Setting setting);

        Task<Setting> DequeueAsync(CancellationToken cancellationToken);
    }
}
