﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.Infrastructure.Caching;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Microsoft.Extensions.Options;

namespace Equinor.ProCoSys.PO.WebApi.Caches
{
    public class PlantCache : IPlantCache
    {
        private readonly ICacheManager _cacheManager;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IPlantApiService _plantApiService;
        private readonly IOptionsMonitor<CacheOptions> _options;

        public PlantCache(
            ICacheManager cacheManager, 
            ICurrentUserProvider currentUserProvider, 
            IPlantApiService plantApiService, 
            IOptionsMonitor<CacheOptions> options)
        {
            _cacheManager = cacheManager;
            _currentUserProvider = currentUserProvider;
            _plantApiService = plantApiService;
            _options = options;
        }

        public async Task<IList<string>> GetPlantIdsForUserOidAsync(Guid userOid)
            => await _cacheManager.GetOrCreate(
                PlantsCacheKey(userOid),
                async () =>
                {
                    var plants = await _plantApiService.GetPlantsAsync();
                    return plants?.Select(p => p.Id).ToList();
                },
                CacheDuration.Minutes,
                _options.CurrentValue.PlantCacheMinutes);

        public async Task<bool> IsValidPlantForUserAsync(string plantId, Guid userOid)
        {
            var plantIds = await GetPlantIdsForUserOidAsync(userOid);
            return plantIds.Contains(plantId);
        }

        public async Task<bool> IsValidPlantForCurrentUserAsync(string plantId)
        {
            var userOid = _currentUserProvider.GetCurrentUserOid();

            return await IsValidPlantForUserAsync(plantId, userOid);
        }

        public void Clear(Guid userOid) => _cacheManager.Remove(PlantsCacheKey(userOid));

        private string PlantsCacheKey(Guid userOid)
            => $"PLANTS_{userOid.ToString().ToUpper()}";
    }
}
