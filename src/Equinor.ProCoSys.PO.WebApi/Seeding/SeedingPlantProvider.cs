using Equinor.ProCoSys.PO.Domain;

namespace Equinor.ProCoSys.PO.WebApi.Seeding
{
    public class SeedingPlantProvider : IPlantProvider
    {
        public SeedingPlantProvider(string plant) => Plant = plant;

        public string Plant { get; }
        public void SetTemporaryPlant(string plant) => throw new System.NotImplementedException();
        public void ReleaseTemporaryPlant() => throw new System.NotImplementedException();
    }
}
