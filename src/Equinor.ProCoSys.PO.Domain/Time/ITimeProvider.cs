using System;

namespace Equinor.ProCoSys.PO.Domain.Time
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}
