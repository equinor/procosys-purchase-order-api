using System.Threading;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.WebApi.Synchronization
{
    public interface ISynchronizationService
    {
        Task Synchronize(CancellationToken cancellationToken);
    }
}
