using Xunit;
using Structum.Elements.Security.Passwords;

namespace Structum.Elements.Security.Tests.Passwords
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="PasswordProtector"/> class.
    /// </summary>
    public class PasswordProtectorTest
    {
        /// <summary>
        ///     Tests the Password Protect and Compare functionality.
        /// </summary>
        [Fact]
        public void RandomProtectAndCompareTest()
        {
            const string passwordToProtect = "MyC0013ncrypti0nP@$$w0rd!";
            using (var protector = new PasswordProtector()) {
                ProtectedPassword protectedPassword = protector.Protect(passwordToProtect);
                Assert.NotNull(protectedPassword);

                Assert.NotNull(protectedPassword.Password);

                Assert.True(protectedPassword.Password.Length <= 44); // The Size of SHA-256
                Assert.True(protectedPassword.Password.Length >= 24); // The Size of MD5

                Assert.NotNull(protectedPassword.Salt);
                Assert.True(protectedPassword.IterationCount > 0);

                Assert.True(protector.CompareProtectedPassword(passwordToProtect, protectedPassword));
            }
        }
    }
}