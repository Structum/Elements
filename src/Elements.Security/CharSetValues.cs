namespace Structum.Elements.Security
{
    /// <summary>
    ///     Defines the character set values.
    /// </summary>
    internal static class CharSetValues
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
    }
}
