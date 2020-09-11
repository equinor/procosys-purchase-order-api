using System.Security.Claims;

namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public interface IClaimsProvider
    {
        ClaimsPrincipal GetCurrentUser();
    }
}
