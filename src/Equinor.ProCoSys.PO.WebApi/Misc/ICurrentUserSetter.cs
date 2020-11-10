using System;

namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public interface ICurrentUserSetter
    {
        void SetCurrentUserOid(Guid oid);
    }
}
