using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Equinor.ProCoSys.PO.Infrastructure
{
    public class DateTimeKindConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeKindConverter() : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        {}
    }
}
