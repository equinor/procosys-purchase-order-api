using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.MainApi.Project
{
    public interface IProjectApiService
    {
        Task<ProCoSysProject> TryGetProjectAsync(string plant, string name);
    }
}
