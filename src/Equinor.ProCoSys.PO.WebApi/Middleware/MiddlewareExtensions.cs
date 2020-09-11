﻿using Microsoft.AspNetCore.Builder;

namespace Equinor.ProCoSys.PO.WebApi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseGlobalExceptionHandling(this IApplicationBuilder app) => app.UseMiddleware<GlobalExceptionHandler>();
        public static void UseCurrentUser(this IApplicationBuilder app) => app.UseMiddleware<CurrentUserMiddleware>();
        public static void UseVerifyOidInDb(this IApplicationBuilder app) => app.UseMiddleware<VerifyOidInDbMiddleware>();
        public static void UseCurrentPlant(this IApplicationBuilder app) => app.UseMiddleware<CurrentPlantMiddleware>();
        public static void UseCurrentBearerToken(this IApplicationBuilder app) => app.UseMiddleware<CurrentBearerTokenMiddleware>();
    }
}
