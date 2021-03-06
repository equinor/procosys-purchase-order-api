﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.PO.MainApi.Permission
{
    public interface IPermissionApiService
    {
        Task<IList<string>> GetPermissionsAsync(string plantId);
        Task<IList<ProCoSysProject>> GetAllOpenProjectsAsync(string plantId);
        Task<IList<string>> GetContentRestrictionsAsync(string plantId);
    }
}
