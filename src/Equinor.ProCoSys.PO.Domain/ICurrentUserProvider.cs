using System;

namespace Equinor.ProCoSys.PO.Domain
{
    public interface ICurrentUserProvider
    {
        Guid GetCurrentUserOid();
    }
}
