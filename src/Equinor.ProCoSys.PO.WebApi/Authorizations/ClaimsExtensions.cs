﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Equinor.ProCoSys.PO.WebApi.Authorizations
{
    public static class ClaimsExtensions
    {
        public const string OidType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public const string GivenNameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        public const string SurNameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

        public static Guid? TryGetOid(this IEnumerable<Claim> claims)
        {
            var oidClaim = claims.SingleOrDefault(c => c.Type == OidType);
            if (Guid.TryParse(oidClaim?.Value, out var oid))
            {
                return oid;
            }

            return null;
        }

        public static string TryGetGivenName(this IEnumerable<Claim> claims)
        {
            var givenName = claims.SingleOrDefault(c => c.Type == GivenNameType);

            return givenName?.Value;
        }

        public static string TryGetSurName(this IEnumerable<Claim> claims)
        {
            var surName = claims.SingleOrDefault(c => c.Type == SurNameType);

            return surName?.Value;
        }
    }
}
