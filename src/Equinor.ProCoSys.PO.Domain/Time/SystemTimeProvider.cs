using System;

namespace Equinor.ProCoSys.PO.Domain.Time
{
    public class SystemTimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
