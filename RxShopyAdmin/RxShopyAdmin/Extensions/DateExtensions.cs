using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RxShopyAdmin
{
    public static class DateExtensions
    {
        public static DateTime IndianTime(this DateTime value)
        {
            try
            {
                string nzTimeZoneKey = "India Standard Time";
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
                return TimeZoneInfo.ConvertTimeFromUtc(value, nzTimeZone);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}