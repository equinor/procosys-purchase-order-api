﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.Domain;
using Equinor.ProCoSys.PO.MainApi.Plant;
using Equinor.ProCoSys.PO.WebApi.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Equinor.ProCoSys.PO.WebApi.Middleware
{
    public class PlantValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public PlantValidatorMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(
            HttpContext context,
            IPlantProvider plantProvider,
            IPlantCache plantCache,
            ILogger<PlantValidatorMiddleware> logger)
        {
            logger.LogInformation($"----- {GetType().Name} start");
            var plantId = plantProvider.Plant;
            if (context.User.Identity.IsAuthenticated && plantId != null)
            {
                if (!await plantCache.IsAValidPlantAsync(plantId))
                {
                    var errors = new Dictionary<string, string[]>
                    {
                        {CurrentPlantMiddleware.PlantHeader, new[] {$"Plant '{plantId}' is not a valid plant"}}
                    };
                    await context.WriteBadRequestAsync(errors, logger);
                    return;
                }
            }

            logger.LogInformation($"----- {GetType().Name} complete");
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
