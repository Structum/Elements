namespace Structum.Elements.Data.Extensions
{
    /// <summary>
    ///     Provides a set of object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Calculates the object's hash using Knuth's algorithm.
        /// </summary>
        /// <param name="obj">Object to hash.</param>
        /// <returns>Hash.</returns>
        public static ulong ComputeKnuthHash(this object obj)
        {
            string json = fastJSON.JSON.ToJSON(obj);
            return json.ComputeKnuthHash();
        }
    }
}