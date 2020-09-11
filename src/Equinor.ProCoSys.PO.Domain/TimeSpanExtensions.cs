using System;

namespace Equinor.ProCoSys.PO.Domain
{
    public static class TimeSpanExtensions
    {
        public static int Weeks(this TimeSpan span) => span.Days / 7;
    }
}
