﻿using System;
using System.Globalization;
using System.Threading;

namespace Equinor.ProCoSys.PO.Domain
{
    public static class DateTimeExtensions
    {
        private static readonly DayOfWeek firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public static DateTime AddWeeks(this DateTime dateTime, int weeks)
            => dateTime.AddDays(7 * weeks);

        public static string FormatAsYearAndWeekString(this DateTime dateTime)
            => string.Concat(ISOWeek.GetYear(dateTime).ToString(), "w", ISOWeek.GetWeekOfYear(dateTime).ToString("00"));

        public static DateTime StartOfWeek(this DateTime dt)
        {
            var dayOfWeek = GetDayOfWeekMondayAsFirst(dt);
            var diff = -(dayOfWeek - (int)firstDayOfWeek);
            return dt.AddDays(diff).Date;
        }

        public static int GetWeeksUntil(this DateTime fromDateTime, DateTime toDateTime)
        {
            var startOfFromWeek = fromDateTime.StartOfWeek();
            var startOfToWeek = toDateTime.StartOfWeek();
            var timeSpan = startOfToWeek - startOfFromWeek;
            
            return timeSpan.Weeks();
        }

        private static int GetDayOfWeekMondayAsFirst(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                // treat Sunday as last day of week since enum DayOfWeek has Sunday as 0
                return 7;
            }

            return (int)dt.DayOfWeek;
        }
    }
}
