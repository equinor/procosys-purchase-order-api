using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.WebApi.Authentication
{
    public interface IApplicationAuthenticator
    {
        ValueTask<string> GetBearerTokenForApplicationAsync();
    }
}
