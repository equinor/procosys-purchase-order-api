using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.MainApi.Responsible
{
    public interface IResponsibleApiService
    {
        Task<ProCoSysResponsible> TryGetResponsibleAsync(string plant, string code);
    }
}
