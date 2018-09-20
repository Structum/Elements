using System;
using System.Text;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Defines the Exception Extensions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Returns the string representation of the exception.
        /// </summary>
        /// <param name="ex">Exception to stringify.</param>
        /// <returns>String representation of the exception.</returns>
        public static string Stringify(this Exception ex)
        {
            var builder = new StringBuilder();

            builder.AppendLine(ex.Message);
            builder.AppendLine(ex.StackTrace);
            builder.AppendLine("");

            if (ex.InnerException == null) {
                return builder.ToString();
            }

            builder.AppendLine(ex.InnerException.Stringify());
            builder.AppendLine("");


            return builder.ToString();
        }
    }
}
