namespace Structum.Elements.Security
{
    /// <summary>
    ///     Provides a set of Characters for comparison.
    /// </summary>
    public static class CharSetValues
    {
        /// <summary>
        ///     Character set to be used when creating passwords. It purposely removes i,I and o,O to
        ///     make sure the password is readable.
        /// </summary>
        public static readonly char[] Password = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '2', '3', '4', '5', '6', '7', '8', '9', '.', '!', '@', '$', '*', '=', '?' };

        /// <summary>
        ///     Special Characters that can be used to create Salts and Passwords.
        /// </summary>
        public static readonly char[] Specials = { '.', '-', '_', '!', '@', '$', '^', '*', '=', '~', '|', '+', '?' };

        /// <summary>
        ///     Alphanumeric Character Set.
        /// </summary>
        public static readonly char[] Alphanumerics = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }
}
