using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api
{
    public static class DateExtensions
    {
        public static DateTime IndianTime(this DateTime value)
        {
            string nzTimeZoneKey = "India Standard Time";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            return TimeZoneInfo.ConvertTimeFromUtc(value, nzTimeZone);
        }
    }
}