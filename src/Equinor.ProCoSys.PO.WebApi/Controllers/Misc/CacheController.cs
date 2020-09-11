﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.MainApi.Permission;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Equinor.ProCoSys.PO.WebApi.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinor.ProCoSys.PO.WebApi.Controllers.Misc
{
    [ApiController]
    [Route("Cache")]
    public class CacheController : ControllerBase
    {
        private readonly IPlantCache _plantCache;
        private readonly IPermissionCache _permissionCache;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IPermissionApiService _permissionApiService;

        public CacheController(IPlantCache plantCache, IPermissionCache permissionCache, ICurrentUserProvider currentUserProvider, IPermissionApiService permissionApiService)
        {
            _plantCache = plantCache;
            _permissionCache = permissionCache;
            _currentUserProvider = currentUserProvider;
            _permissionApiService = permissionApiService;
        }

        [Authorize]
        [HttpPut("Clear")]
        public void Clear(
            [FromHeader(Name = CurrentPlantMiddleware.PlantHeader)]
            [Required]
            [StringLength(PlantEntityBase.PlantLengthMax, MinimumLength = PlantEntityBase.PlantLengthMin)]
            string plant)
        {
            var currentUserOid = _currentUserProvider.GetCurrentUserOid();
            _plantCache.Clear(currentUserOid);
            _permissionCache.ClearAll(plant, currentUserOid);
        }

        [Authorize]
        [HttpGet("PermissionsFromCache")]
        public async Task<IList<string>> GetPermissions(
            [FromHeader(Name = CurrentPlantMiddleware.PlantHeader)]
            [Required]
            string plant)
        {
            var currentUserOid = _currentUserProvider.GetCurrentUserOid();
            var permissions = await _permissionCache.GetPermissionsForUserAsync(plant, currentUserOid);
            return permissions;
        }

        [Authorize]
        [HttpGet("PermissionsFromMain")]
        public async Task<IList<string>> GetPermissionsFromMain(
            [FromHeader(Name = CurrentPlantMiddleware.PlantHeader)]
            [Required]
            string plant)
        {
            var permissions = await _permissionApiService.GetPermissionsAsync(plant);
            return permissions;
        }

        [Authorize]
        [HttpGet("ProjectsFromCache")]
        public async Task<IList<string>> GetProjects(
            [FromHeader(Name = CurrentPlantMiddleware.PlantHeader)]
            [Required]
            string plant)
        {
            var currentUserOid = _currentUserProvider.GetCurrentUserOid();
            var projects = await _permissionCache.GetProjectNamesForUserOidAsync(plant, currentUserOid);
            return projects;
        }

        [Authorize]
        [HttpGet("PlantsFromCache")]
        public async Task<IList<string>> GetPlants()
        {
            var currentUserOid = _currentUserProvider.GetCurrentUserOid();
            var plants = await _plantCache.GetPlantIdsForUserOidAsync(currentUserOid);
            return plants;
        }
    }
}
