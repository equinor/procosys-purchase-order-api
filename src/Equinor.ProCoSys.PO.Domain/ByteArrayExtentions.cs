using System;

namespace Equinor.ProCoSys.PO.Domain
{
    public static class ByteArrayExtensions
    {
        public static string ConvertToString(this byte[] bytes) => Convert.ToBase64String(bytes);
    }
}
