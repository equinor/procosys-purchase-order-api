using System;
using System.Net;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.MainApi.Client;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Microsoft.Extensions.Options;

namespace Equinor.ProCoSys.PO.MainApi.Responsible
{
    public class MainApiResponsibleService : IResponsibleApiService
    {
        private readonly string _apiVersion;
        private readonly Uri _baseAddress;
        private readonly IBearerTokenApiClient _mainApiClient;
        private readonly IPlantCache _plantCache;

        public MainApiResponsibleService(IBearerTokenApiClient mainApiClient,
            IPlantCache plantCache,
            IOptionsMonitor<MainApiOptions> options)
        {
            _mainApiClient = mainApiClient;
            _plantCache = plantCache;
            _apiVersion = options.CurrentValue.ApiVersion;
            _baseAddress = new Uri(options.CurrentValue.BaseAddress);
        }

        public async Task<ProCoSysResponsible> TryGetResponsibleAsync(string plant, string code)
        {
            if (!await _plantCache.HasCurrentUserAccessToPlantAsync(plant))
            {
                throw new ArgumentException($"Invalid plant: {plant}");
            }

            var url = $"{_baseAddress}Library/Responsible" +
                      $"?plantId={plant}" +
                      $"&code={WebUtility.UrlEncode(code)}" +
                      $"&api-version={_apiVersion}";

            return await _mainApiClient.TryQueryAndDeserializeAsync<ProCoSysResponsible>(url);
        }
    }
}
