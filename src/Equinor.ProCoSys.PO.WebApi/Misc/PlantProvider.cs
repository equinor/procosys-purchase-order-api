using Equinor.ProCoSys.PO.Domain;

namespace Equinor.ProCoSys.PO.WebApi.Misc
{
    public class PlantProvider : IPlantProvider, IPlantSetter
    {
        public string Plant { get; private set; }

        public void SetPlant(string plant) => Plant = plant;
    }
}
