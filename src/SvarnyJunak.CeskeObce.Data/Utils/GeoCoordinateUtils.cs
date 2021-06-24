using System;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public static class GeoCoordinateUtils
    {
        /// <summary>
        /// Returns distance in meters from one GPS point to another.
        /// </summary>
        public static decimal GetDistance(decimal longitude, decimal latitude, decimal otherLongitude, decimal otherLatitude)
        {
            return (decimal)GetDistance((double)longitude, (double)latitude, (double)otherLongitude, (double)otherLatitude);
        }

        /// <summary>
        /// Returns distance in meters from one GPS point to another.
        /// </summary>
        public static double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
