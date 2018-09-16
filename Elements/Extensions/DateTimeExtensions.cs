using System;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Defines the Date Time Extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Returns a timestamp.
        /// </summary>
        /// <param name="value">Current Date Time Value.</param>
        /// <returns>Timestamp.</returns>
        public static string GetTimestamp(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        ///     Returns true if the Date to compare is equal to the current Date.
        /// </summary>
        /// <param name="value">Current Date.</param>
        /// <param name="valueToCompare">Date to compare.</param>
        /// <returns>True if the Date to compare is equal to the current Date.</returns>
        public static bool IsEqualTo(this DateTime value, DateTime valueToCompare)
        {
            return value.GetTimestamp().Equals(valueToCompare.GetTimestamp());
        }

        /// <summary>
        ///     Returns the unix timestamp.
        /// </summary>
        /// <param name="value">Current Date Time Value.</param>
        /// <returns>Unix Timestamp.</returns>
        public static int ToUnixTimestamp(this DateTime value)
        {
            return (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
