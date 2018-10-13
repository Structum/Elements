using Structum.Elements.Data.HashFunctions;

namespace Structum.Elements.Data.Extensions
{
    /// <summary>
    ///     Provides a set of string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Calculate Knuth's hash on the selected string.
        /// </summary>
        /// <param name="subject">String to hash.</param>
        /// <returns>Hash of subject.</returns>
        public static ulong ComputeKnuthHash(this string subject)
        {
            return new KnuthHash().ComputeHash(subject);
        }
    }
}