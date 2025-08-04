using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public static class UrlHelperExtensions
    {
        public static string QualifiedRouteSecured(this IUrlHelper url, string routeName, object routeValues = null)
        {
            return url.RouteUrl(routeName, routeValues, "https");
        }
    }

    public static class DateTimeExtensions
    {
        public static string DisplayDate(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            return dateTime.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
        }

        public static string DisplayTime(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            return dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }

        public static string DisplayDateTime(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            return dateTime.ToString("MM-dd-yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        public static string DisplayDateTime_History(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            //  April 19, 2023 at 10:00AM
            return dateTime.ToString("MMMM dd, yyyy 'at' hh:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
