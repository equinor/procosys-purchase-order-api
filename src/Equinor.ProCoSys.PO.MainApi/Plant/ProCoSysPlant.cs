﻿using System.Diagnostics;

namespace Equinor.ProCoSys.PO.MainApi.Plant
{
    [DebuggerDisplay("{Title} {Id} {HasAccess}")]
    public class ProCoSysPlant
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool HasAccess { get; set; }
    }
}
