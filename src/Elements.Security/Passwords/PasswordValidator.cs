
using Structum.Elements.Extensions;
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
    ///     PasswordStrength strength = PasswordValidator.GetPasswordStrength("god", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Bad.
    ///     Console.WriteLine(message); // This will be: The password needs to be at least 8 characters long.
    ///
    ///     strength = PasswordValidator.GetPasswordStrength("SuperMan1", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Weak.
    ///     Console.WriteLine(message); // This will be: The password needs to have at least one number.
    /// 
    ///     strength = PasswordValidator.GetPasswordStrength("myP@ss1234!", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Weak.
    ///     Console.WriteLine(message); // This will be: The password needs to have at least one special character.
    ///
    ///     strength = PasswordValidator.GetPasswordStrength("C4ll m3 m@yb3!", out message);
    ///     Console.WriteLine(Enum.GetName(typeof(PasswordStrength), strength); // Best.
    ///     </code>
    /// </example>
    public static class PasswordValidator
    {
        /// <summary>
        ///     Returns the password strength for the selected password.
        /// </summary>
        /// <remarks>
        ///     Password Strength: <br />
        ///     <ul>
        ///         <li>Bad: Bad password. Improve it by adding a number.</li>
        ///         <li>Weak: Weak Password. Adding an Upper case character and special character will improve it.</li>
        ///         <li>Strong: Strong Password. Improve it by adding spaces.</li>
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

            if (!password.ContainsNumbers()) {
                messages = "The password needs to have at least one number.";
                return PasswordStrength.Bad;
            }

            if (!password.ContainsUpperCaseChars()) {
                messages = "The password needs to have at least one upper case character.";
                return PasswordStrength.Weak;
            }

            if (!password.ContainsSpecialChars()) {
                messages = "The password needs to have at least one special character.";
                return PasswordStrength.Weak;
            }

            if (Regex.IsMatch(password, best) && password.ContainsSpaces()) {
                return PasswordStrength.Best;
            }

            if (Regex.IsMatch(password, strong)) {
                return PasswordStrength.Strong;
            }

            if (Regex.IsMatch(password, weak)) {
                return PasswordStrength.Weak;
            }

            if (Regex.IsMatch(password, bad)) {
                messages = "The password needs to have at least one number.";
                return PasswordStrength.Bad;
            }

            return PasswordStrength.Bad;
        }
    }
}
