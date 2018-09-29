using Xunit;
using Structum.Elements.Security.Encryption;

namespace Structum.Elements.Security.Tests.Encryption
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="Hasher"/> class.
    /// </summary>
    public class HasherTest
    {
        /// <summary>
        ///     Tests the Default configuration for the compute a hash.
        /// </summary>
        [Fact]
        public void DefaultComputeHashTest()
        {
            var hasher = new Hasher();
            string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
            Assert.Equal(44, hash.Length);
        }

        /// <summary>
        ///     Tests the compute a hash using the MD5 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Md5ComputeHashTest()
        {
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Md5
            };
            string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
            Assert.Equal(24, hash.Length);
        }

        /// <summary>
        ///     Tests the compute a hash using the SHA1 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Sha1ComputeHashTest()
        {
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Sha1
            };
            string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
            Assert.Equal(28, hash.Length);
        }

        /// <summary>
        ///     Tests the compute a hash using the SHA256 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Sha256ComputeHashTest()
        {
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Sha256
            };
            string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
            Assert.Equal(44, hash.Length);
        }
    }
}