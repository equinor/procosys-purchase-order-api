﻿using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Equinor.ProCoSys.PO.WebApi.Middleware
{
    public static class ApiDescriptionExtension
    {
        public static AuthorizeAttribute[] GetAuthorizeAttributes(this ApiDescription apiDescription)
        {
            var hasAuthAttribute = apiDescription.ActionDescriptor.EndpointMetadata
                .Where(i => i.GetType() == typeof(AuthorizeAttribute) || i.GetType().BaseType == typeof(AuthorizeAttribute))
                .Cast<AuthorizeAttribute>();

            return hasAuthAttribute as AuthorizeAttribute[] ?? hasAuthAttribute.ToArray();
        }
    }
}
