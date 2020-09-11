using System;
using System.Threading.Tasks;
using Equinor.ProCoSys.PO.WebApi.Authorizations;
using Equinor.ProCoSys.PO.WebApi.Misc;
using Microsoft.AspNetCore.Http;

namespace Equinor.ProCoSys.PO.WebApi.Middleware
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor, ICurrentUserSetter currentUserSetter)
        {
            var oid = httpContextAccessor.HttpContext.User.Claims.TryGetOid();
            if (oid.HasValue)
            {
                currentUserSetter.SetCurrentUser(oid.Value);
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
