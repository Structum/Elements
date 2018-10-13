namespace Structum.Elements.Data.HashFunctions
{
    /// <summary>
    ///     Provides an implementation of the Jenkins One at a time Hash.
    /// </summary>
    /// <remarks>
    ///     The implementation of Jenkins One at a time can be found here: <see href="https://en.wikipedia.org/wiki/Jenkins_hash_function"/>.
    ///     This implementation was ported from the link above. The algorithm is fully described here:
    ///     <see href="http://www.burtleburtle.net/bob/hash/doobs.html"/>.
    /// </remarks>
    public class JenkinsHash : IHashFunction<string, ulong>
    {
        /// <inheritdoc />
        public ulong ComputeHash(string input)
        {
            return this.ComputeHash(System.Text.Encoding.Unicode.GetBytes(input));
        }

        /// <summary>
        ///     Computes the hash of the selected input.
        /// </summary>
        /// <param name="input">The input to compute the hash for.</param>
        /// <returns>Computed Hash.</returns>
        public ulong ComputeHash(byte[] input)
        {
            uint hash = 0;
            foreach (byte b in input) {
                hash += b;
                hash += hash << 10;
                hash ^= hash >> 6;
            }

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return hash;
        }
    }
}