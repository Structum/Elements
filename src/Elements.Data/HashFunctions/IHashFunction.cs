namespace Structum.Elements.Data.HashFunctions
{
    /// <summary>
    ///     Provides a common interface to implement Non-Cryptographic Hash Functions.
    /// </summary>
    /// <typeparam name="T">Input Type.</typeparam>
    /// <typeparam name="TResult">Result Type.</typeparam>
    public interface IHashFunction<in T, out TResult>
    {
        /// <summary>
        ///     Computes the hash of the selected input.
        /// </summary>
        /// <param name="input">The input to compute the hash for.</param>
        /// <returns>Computed Hash.</returns>
        TResult ComputeHash(T input);
    }
}