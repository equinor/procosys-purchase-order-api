﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.Domain
{
    public interface IPermissionCache
    {
        Task<IList<string>> GetPermissionsForUserAsync(string plantId, Guid userOid);
        Task<IList<string>> GetProjectsForUserAsync(string plantId, Guid userOid);
        Task<IList<string>> GetContentRestrictionsForUserAsync(string plantId, Guid userOid);
        Task<bool> IsAValidProjectAsync(string plantId, Guid userOid, string projectName);
        void ClearAll(string plantId, Guid userOid);
    }
}
