using System;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Provides Date Time extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Returns a timestamp representation of the selected date with format "yyyyMMddHHmmssffff". e.g. 2018022517351415
        /// </summary>
        /// <param name="value">Selected Date Time Value.</param>
        /// <returns>Timestamp.</returns>
        public static string GetTimestamp(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        ///     Returns <c>true</c> if the Date to compare is equal to the current Date.
        /// </summary>
        /// <param name="value">Selected Date.</param>
        /// <param name="valueToCompare">Date to compare.</param>
        /// <returns><c>true</c> if the Date to compare is equal to the current Date.</returns>
        public static bool IsEqualTo(this DateTime value, DateTime valueToCompare)
        {
            return value.GetTimestamp().Equals(valueToCompare.GetTimestamp());
        }

        /// <summary>
        ///     Returns the unix timestamp representation of the selected date. Ticks since 1970-01-01.
        /// </summary>
        /// <param name="value">Selected Date Time Value.</param>
        /// <returns>Unix Timestamp.</returns>
        public static int ToUnixTimestamp(this DateTime value)
        {
            return (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
