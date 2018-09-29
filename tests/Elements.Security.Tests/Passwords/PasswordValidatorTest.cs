using Xunit;
using Structum.Elements.Security.Passwords;

namespace Structum.Elements.Security.Tests.Passwords
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="PasswordValidator"/> class.
    /// </summary>
    public class PasswordValidatorTest
    {
        /// <summary>
        ///     Tests the Password Strength functionality.
        /// </summary>
        [Fact]
        public void PasswordStrengthTest()
        {
            const string badPasswd = "god";
            const string weakPasswd = "SuperMan1";
            const string strongPasswd = "myP@ss1234!";
            const string bestPasswd = "C4ll m3 m@yb3!";

            string msg = string.Empty;
            Assert.True(PasswordStrength.Bad == PasswordValidator.GetPasswordStrength(badPasswd, out msg), msg);

            msg = string.Empty;
            Assert.True(PasswordStrength.Weak == PasswordValidator.GetPasswordStrength(weakPasswd, out msg), msg);

            msg = string.Empty;
            Assert.True(PasswordStrength.Strong == PasswordValidator.GetPasswordStrength(strongPasswd, out msg), msg);

            msg = string.Empty;
            Assert.True(PasswordStrength.Best == PasswordValidator.GetPasswordStrength(bestPasswd, out msg), msg);
        }
    }
}