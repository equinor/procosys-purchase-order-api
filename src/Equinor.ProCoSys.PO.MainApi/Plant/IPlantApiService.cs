using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.MainApi.Plant
{
    public interface IPlantApiService
    {
        Task<List<ProCoSysPlant>> GetPlantsAsync();
    }
}
