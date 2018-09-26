using System.Linq;
using System.Text.RegularExpressions;

namespace Structum.Elements.Security.Passwords
{
    /// <summary>
    ///     Provides a set of methods to measure the strength of passwords.
    /// </summary>
    /// <example>
    ///     The following code demonstrates how to use the Password Validator:
    ///     <code>
    ///     string message;
    ///     PasswordStrength strength = PasswordValidator.GetPasswordStrength("mypwd", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Bad.
    ///     Console.WriteLine(message); // This will be: The password needs to be at least 8 characters long.
    ///
    ///     strength = PasswordValidator.GetPasswordStrength("mypassword", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Bad.
    ///     Console.WriteLine(message); // This will be: The password needs to have at least one number.
    /// 
    ///     strength = PasswordValidator.GetPasswordStrength("mypassword1", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Bad.
    ///     Console.WriteLine(message); // This will be: The password needs to have at least one special character.
    ///
    ///     strength = PasswordValidator.GetPasswordStrength("mypassword1", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Strong.
    ///     </code>
    /// </example>
    public static class PasswordValidator
    {
        /// <summary>
        ///     Returns the password strength.
        /// </summary>
        /// <remarks>
        ///     Password Strength: <br />
        ///     <ul>
        ///         <li>Bad: The password needs to be at least 8 characters long, contain one number and one special character.</li>
        ///         <li>Weak: Weak Password. </li>
        ///         <li>Strong: Strong Password.</li>
        ///         <li>Best: Best Password.</li>
        ///     </ul>
        /// </remarks>
        /// <param name="password">Password to check.</param>
        /// <param name="messages">Messages.</param>
        /// <returns><c>PasswordStrength</c></returns>
        public static PasswordStrength GetPasswordStrength(string password, out string messages)
        {
            messages = string.Empty;

            const string noOfChars = @"^.*(?=.{8,}).*$";
            const string best = @"^.*(?=.{8,})(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$";
            const string strong = @"^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{8,})(((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\d]))|((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\W_]))|((?=[a-zA-Z\d\W_]*[\d])(?=[a-zA-Z\d\W_]*[\W_])))[a-zA-Z\d\W_]*$";
            const string weak = @"^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{8,})(?=[a-zA-Z\d\W_]*[A-Z]|[a-zA-Z\d\W_]*[\d]|[a-zA-Z\d\W_]*[\W_])[a-zA-Z\d\W_]*$";
            const string bad = @"^((^[a-z]{8,}$)|(^[A-Z]{8,}$)|(^[\d]{8,}$)|(^[\W_]{8,}$))$";

            if (!Regex.IsMatch(password, noOfChars)) {
                messages = "The password needs to be at least 8 characters long.";
                return PasswordStrength.Bad;
            }

            if (Regex.IsMatch(password, bad)) {
                messages = "The password needs to have at least one number.";
                return PasswordStrength.Bad;
            }

            if (CharSetValues.Specials.Any(password.Contains)) {
                messages = "The password needs to have at least one special character.";
                return PasswordStrength.Bad;
            }

            if (Regex.IsMatch(password, best)) {
                return PasswordStrength.Best;
            }

            if (Regex.IsMatch(password, strong)) {
                return PasswordStrength.Strong;
            }

            if (Regex.IsMatch(password, weak)) {
                return PasswordStrength.Weak;
            }

            return PasswordStrength.Bad;
        }
    }
}
