using System.Threading;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.Domain
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
