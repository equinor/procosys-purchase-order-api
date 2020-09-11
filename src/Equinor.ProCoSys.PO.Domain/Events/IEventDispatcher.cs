using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.Domain.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IEnumerable<EntityBase> entities, CancellationToken cancellationToken = default);
    }
}
