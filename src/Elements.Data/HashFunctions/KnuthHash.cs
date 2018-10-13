namespace Structum.Elements.Data.HashFunctions
{
    /// <summary>
    ///     Provides an implement of Knuth's Hash Function.
    /// </summary>
    public class KnuthHash : IHashFunction<string, ulong>
    {
        /// <inheritdoc />
        public ulong ComputeHash(string input)
        {
            ulong hashedValue = 3074457345618258791ul;
            foreach (char c in input) {
                hashedValue += c;
                hashedValue *= 3074457345618258799ul;
            }

            return hashedValue;
        }
    }
}