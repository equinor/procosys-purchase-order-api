using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.MainApi.Client
{
    public interface IBearerTokenProvider
    {
        ValueTask<string> GetBearerTokenOnBehalfOfCurrentUserAsync();
    }
}
