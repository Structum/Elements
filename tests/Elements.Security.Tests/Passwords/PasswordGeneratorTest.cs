using Xunit;
using System;
using Structum.Elements.Extensions;
using Structum.Elements.Security.Passwords;

namespace Structum.Elements.Security.Tests.Passwords
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="PasswordGenerator"/> class.
    /// </summary>
    public class PasswordGeneratorTest : IDisposable
    {
        /// <summary>
        ///     Internal Randomizer.
        /// </summary>
        private readonly Randomizer _randomizer = new Randomizer();

        /// <summary>
        ///     Tests the Default configuration for the Generate Password functionality.
        /// </summary>
        [Fact]
        public void DefaultGenerateTest()
        {
            PasswordGenerator generator = new PasswordGenerator(this._randomizer);

            string generatedPwd = generator.Generate();
            Assert.False(string.IsNullOrEmpty(generatedPwd));
            Assert.True(PasswordGenerator.DefaultMinimum <= generatedPwd.Length, generatedPwd.Length.ToString());
            Assert.True(PasswordGenerator.DefaultMaximum >= generatedPwd.Length, generatedPwd.Length.ToString());

            Assert.True(generatedPwd.ContainsSpecialChars());
            Assert.True(generatedPwd.ContainsNumbers());
            Assert.True(generatedPwd.ContainsLowerCaseChars());
            Assert.True(generatedPwd.ContainsUpperCaseChars());
        }

        /// <summary>
        ///     Tests the Generate Password functionality with custom sizes.
        /// </summary>
        [Fact]
        public void CustomSizesGenerateTest()
        {
            PasswordGenerator generator = new PasswordGenerator(this._randomizer) {
                MinimumSize = 12,
                MaximumSize = 17
            };

            string generatedPwd = generator.Generate();
            Assert.False(string.IsNullOrEmpty(generatedPwd));
            Assert.True(generator.MinimumSize <= generatedPwd.Length, generatedPwd.Length.ToString());
            Assert.True(generator.MaximumSize >= generatedPwd.Length, generatedPwd.Length.ToString());

            Assert.True(generatedPwd.ContainsSpecialChars());
            Assert.True(generatedPwd.ContainsNumbers());
            Assert.True(generatedPwd.ContainsLowerCaseChars());
            Assert.True(generatedPwd.ContainsUpperCaseChars());
        }

        /// <summary>
        ///     Disposes the tests resources.
        /// </summary>
        public void Dispose()
        {
            this._randomizer?.Dispose();
        }
    }
}