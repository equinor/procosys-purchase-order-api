using System;

namespace Equinor.ProCoSys.PO.WebApi.Synchronization
{
    public class SynchronizationOptions
    {
        public TimeSpan Interval { get; set; }
        public Guid UserOid { get; set; }
    }
}
